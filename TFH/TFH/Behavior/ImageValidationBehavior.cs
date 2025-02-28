using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFH.Behavior
{
    public class ImageValidationBehavior : Behavior<Image>
    {
       // public static readonly BindableProperty AttachBehaviorProperty =
       //BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(ImageValidationBehavior), false, propertyChanged: OnAttachBehaviorChanged);

       // public static bool GetAttachBehavior(BindableObject view)
       // {
       //     return (bool)view.GetValue(AttachBehaviorProperty);
       // }

       // public static void SetAttachBehavior(BindableObject view, bool value)
       // {
       //     view.SetValue(AttachBehaviorProperty, value);
       // }

       // static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
       // {
       //     Image image = view as Image;
       //     if (image == null)
       //     {
       //         return;
       //     }

       //     bool attachBehavior = (bool)newValue;
       //     if (attachBehavior)
       //     {
       //         image.Behaviors.Add(new ImageValidationBehavior());
       //     }
       //     else
       //     {
       //         var toRemove = image.Behaviors.FirstOrDefault(b => b is ImageValidationBehavior);
       //         if (toRemove != null)
       //         {
       //             image.Behaviors.Remove(toRemove);
       //         }
       //     }
       // }

        protected override void OnAttachedTo(Image image)
        {
            if (image.Source == null)
                image.Source = "";
            image.Source.PropertyChanged += Source_PropertyChanged;
            base.OnAttachedTo(image);
        }        

        protected override void OnDetachingFrom(Image image)
        {
            if (image.Source == null)
                image.Source = "";
            image.Source.PropertyChanged -= Source_PropertyChanged;
            base.OnDetachingFrom(image);
        }

        private void Source_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            bool Valid = true;
            try
            {
                FileInfo fileInfo = new FileInfo(e.ToString());
                if (fileInfo.Exists)
                {
                    if (fileInfo.Length > 2000000)
                    {
                        Valid = false;
                    }
                    else
                    {
                        using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(e.ToString()))
                        {
                            if (bmp.Height > 200 || bmp.Width > 200)
                            {
                                Valid = false;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Valid = false;
                //throw; 
            }
            if (!Valid)
                ((Image)sender).Source = "";
            else
                ((Image)sender).Source = e.ToString();
        }

    }
}
