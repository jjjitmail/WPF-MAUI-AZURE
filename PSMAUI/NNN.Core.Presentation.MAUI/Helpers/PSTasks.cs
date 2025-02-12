//using PSTouchExpress.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Helpers
{
    public static class PSTasks
    {
        public static T GetChild<T>(Element element, string childName) where T : Element
        {
            var properties = element.GetType().GetRuntimeProperties();

            T _element = null;
            var childrenProperty = properties.FirstOrDefault(w => w.Name == "Children");
            if (childrenProperty != null)
            {
                // loop through children
                IEnumerable children = childrenProperty.GetValue(element) as IEnumerable;
                foreach (var child in children)
                {
                    var childVisualElement = child as Element;
                    if (childVisualElement != null)
                    {
                        // return match
                        if (childVisualElement is T)
                        {
                            var label_properties = element.GetType().GetProperties();
                            var labelProperty = properties.FirstOrDefault(w => w.Name == childName);
                            if (labelProperty != null)
                            {
                                _element = labelProperty.GetValue(child) as T;
                                break;
                                //yield return childVisualElement as T;
                            }
                            //yield return childVisualElement as T;
                        }
                        var foundChild = GetChild<T>(childVisualElement, childName);
                        if (foundChild != null) break;                        
                    }
                }

            }
            return _element;
        }

        public static IEnumerable<T> GetChildren<T>(this Element element, string childName) where T : Element
        {
            var properties = element.GetType().GetRuntimeProperties();

            IEnumerable<T> _element = null;
            var childrenProperty = properties.FirstOrDefault(w => w.Name == "Children");
            if (childrenProperty != null)
            {
                // loop through children
                IEnumerable children = childrenProperty.GetValue(element) as IEnumerable;
                foreach (var child in children)
                {
                    var childVisualElement = child as Element;
                    if (childVisualElement != null)
                    {
                        // return match
                        if (childVisualElement is T)
                        {
                            var label_properties = element.GetType().GetProperties();
                            var labelProperty = properties.FirstOrDefault(w => w.Name == childName);
                            if (labelProperty != null)
                            {
                                yield return childVisualElement as T;
                            }
                            //yield return childVisualElement as T;
                        }

                        // return recursive results of children
                        foreach (var childVisual in childVisualElement.GetChildren<T>(childName))
                        {
                            yield return childVisual;
                        }
                    }
                }
                
            }
        }
    

        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static Task ActionTask(Action action)
        {
            Func<Task> task = () => {
                action();
                return Task.CompletedTask;
            };
            return task();
        }

        //public static async Task<Model> BindContext<IPage, Model>(this ContentView ContentPresenter, Func<Task> CallBack = null)
        //    where Model : IPageModel
        //    where IPage : ContentView
        //{
        //    var model = Activator.CreateInstance<Model>();
        //    await model.BtnPressAction(async () =>
        //    {
        //        await PSTasks.ActionTask(() =>
        //        {
        //            ContentView currentpage = Activator.CreateInstance<IPage>();
        //            currentpage.BindingContext = model;
        //            ContentPresenter = currentpage;
        //        });
        //    });
        //    CallBack?.Invoke();
        //    return model;
        //    //return Task.CompletedTask;
        //}
    }
}
