using System.Threading.Tasks;

namespace PSTouchExpress.Behaviors
{
	[Preserve(AllMembers = true)]
	public interface IAction
	{
		Task<bool> Execute(object sender, object parameter);
	}
}
