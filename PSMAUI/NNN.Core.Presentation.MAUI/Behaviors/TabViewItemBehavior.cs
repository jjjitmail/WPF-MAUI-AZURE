using NNN.Core.Presentation.MAUI.Helpers;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Behaviors
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
