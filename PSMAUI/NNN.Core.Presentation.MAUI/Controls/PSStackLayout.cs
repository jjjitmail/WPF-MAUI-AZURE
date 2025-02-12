using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Controls
{
    public class PSStackLayout : StackLayout
    {
        TapGestureRecognizer _tap = new TapGestureRecognizer();

        public PSStackLayout()
        {
            _tap.Tapped += StackLayout_Clicked;
        }

        private void StackLayout_Clicked(object sender, EventArgs e)
        {
            TimeSpan tt = new TimeSpan(0, 0, 0, 0, 250);
            Device.StartTimer(tt, ClickHandle);
        }

        bool ClickHandle()
        {
            //tag = "Clicked Once";
            return false;
        }

        ~PSStackLayout() => Dispose(false);

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _tap.Tapped -= StackLayout_Clicked;
            }
        }
    }
}
