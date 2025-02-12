using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Behaviors
{
	[Preserve(AllMembers = true)]
	public interface IAction
	{
		Task<bool> Execute(object sender, object parameter);
	}
}
