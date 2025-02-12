using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMDD.Models
{
    public static class Navigator
    {
        private static List<Action> PageActionList = new List<Action>();

        public static void Register(Action action)
        {
            if (PageActionList.Count(t=> t.Method.Name == action.Method.Name) < 1)
            {
                PageActionList.Add(action);
            }
        }

        public static void UnRegister(Action action)
        {
            PageActionList.Remove(action);
        }

        public static void Call(Action action)
        {
            var _action = PageActionList.FirstOrDefault(t=> t.Method.Name == action.Method.Name);
            if (_action != null)
                _action();
        }
    }
}
