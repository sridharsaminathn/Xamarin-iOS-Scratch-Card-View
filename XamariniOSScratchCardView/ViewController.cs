using Foundation;
using System;
using UIKit;

namespace XamariniOSScratchCardView
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            scratchArea.Image = UIImage.FromBundle("scratch_img");
            scratchArea.BackgroundColor = UIColor.White.ColorWithAlpha(0.5f);
            scratchArea.ScratchCompleted += ScratchArea_ScratchCompleted;
        }

        private void ScratchArea_ScratchCompleted(int obj)
        {
            lblScratchPercentage.Text = string.Format("Scratch {0}% Completed", obj);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}