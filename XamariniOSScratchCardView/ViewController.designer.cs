// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace XamariniOSScratchCardView
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView after_scratching { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblScratchPercentage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        XamariniOSScratchCardView.ScratchCardView scratchArea { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (after_scratching != null) {
                after_scratching.Dispose ();
                after_scratching = null;
            }

            if (lblScratchPercentage != null) {
                lblScratchPercentage.Dispose ();
                lblScratchPercentage = null;
            }

            if (scratchArea != null) {
                scratchArea.Dispose ();
                scratchArea = null;
            }
        }
    }
}