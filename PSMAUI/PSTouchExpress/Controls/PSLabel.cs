using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Controls
{
    public class PSLabel : Label, IDisposable
    {
        TapGestureRecognizer singleTap = new TapGestureRecognizer()
        {
            NumberOfTapsRequired = 1, 
        };
        //TapGestureRecognizer doubleTap = new TapGestureRecognizer()
        //{
        //    NumberOfTapsRequired = 2
        //};
        public PSLabel()
        {            
            this.GestureRecognizers.Add(singleTap);
            //this.GestureRecognizers.Add(doubleTap);
            singleTap.Tapped += Label_Clicked;
            //doubleTap.Tapped += Label_Clicked;
        }

        //private static int clickCount;

        private void Label_Clicked(object sender, EventArgs e)
        {
            //if (clickCount < 1)
            //{
                TimeSpan tt = new TimeSpan(0, 0, 0, 0, 250);
                Device.StartTimer(tt, ClickHandle);
            //}
            //clickCount++;
        }

        bool ClickHandle()
        {
            ClickedOnce();
            //if (clickCount > 1)
            //{
            //    ClickedTwice();
            //}
            //else
            //{
            //    ClickedOnce();
            //}
            //clickCount = 0;
            return false;
        }

        private void ClickedTwice()
        {
            Text = "Clicked Twice";
        }

        private void ClickedOnce()
        {
            FontAttributes = FontAttributes.Bold;
            //Text = "Clicked Once"; //(Convert.ToInt16(Text) + 1).ToString();
        }

        ~PSLabel() => Dispose(false);

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
                singleTap.Tapped -= Label_Clicked;
                //doubleTap.Tapped -= Label_Clicked;
            }
        }
    }
}
