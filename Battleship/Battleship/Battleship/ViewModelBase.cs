using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Battleship
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Set<T>(ref T field, T value, [CallerMemberName] string propName = "")
		{
			if (!field.Equals(value))
			{
				field = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
			}
		}
	}
}