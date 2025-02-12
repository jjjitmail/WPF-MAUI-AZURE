using PSTouchExpress.Helpers;
using PSTouchExpress.Views;
using System.Threading.Tasks;

namespace PSTouchExpress.Behaviors
{
    [Preserve(AllMembers = true)]
    public class TabViewItemBehavior : AnimationBase, IAction
    {
        public async Task<bool> Execute(object sender, object parameter)
        {
            var obj = sender;
            await PSTasks.ActionTask(() =>
            {
                var ele = sender;

            });

            return true;
        }
    }
}
