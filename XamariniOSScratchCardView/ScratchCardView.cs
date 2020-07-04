// #region FileHeader
// // ***********************************************************************
// // Assembly       : /Users/mnameit/Projects/XamariniOSScratchCardView/XamariniOSScratchCardView
// // Author            : Sridhar Saminathan (598197)
// // Created          : 04-07-2020
// //
// // ***********************************************************************
// // <copyright file="ScratchCardView.cs" company="CTS">
// //     Copyright ©  2020
// // </copyright>
// // <summary></summary>
// // ***********************************************************************
// #endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;

namespace XamariniOSScratchCardView
{
    [Register("ScratchCardView"), DesignTimeVisible(true)]
    public class ScratchCardView : UIView
    {
        #region Declarations

        CGPoint startPoint;

        CGPoint endPoint;

        CGContext context;

        CGRect currentFrame;

        List<ScratchedPoints> co_ordinates = new List<ScratchedPoints>();

        bool swiped = false;

        bool IsCallbackCompleted = false;

        #endregion

        int min_x = 1000;
        int max_x = 0;

        int min_y = 1000;
        int max_y = 0;


        #region Bindable Properties

        UIImage image;
        [Export("Image"), Browsable(true)]
        public UIImage Image
        {
            get { return image; }
            set
            {
                image = value;
                SetNeedsDisplay();
            }
        }

        public event Action<int> ScratchCompleted;

        public int ScrachRevealMinPercent { get; set; }

        #endregion

        #region Constructors
        public ScratchCardView(IntPtr p)
            : base(p)
        {
            Initialize();
        }

        public ScratchCardView()
        {
            Initialize();
        }
        #endregion

        private void Initialize()
        {
            co_ordinates.Add(new ScratchedPoints { startPoint = startPoint, endPoint = endPoint });
            //Image = UIImage.FromBundle(LangUtils.IsArabic ? PclImages.ScratchCardIconAr : PclImages.ScratchCardIcon);
            ScrachRevealMinPercent = 25;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            CalculateUserScratchArea();

            context = UIGraphics.GetCurrentContext();

            if (image != null)
            {
                image.Draw(new CGRect(0, 0, this.Frame.Width, this.Frame.Height));
                currentFrame = new CGRect(0, 0, this.Frame.Width, this.Frame.Height);
            }

            foreach (var item in co_ordinates)
            {
                this.DrawLineFrom(item.startPoint, item.endPoint);
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            var touch = touches.AnyObject as UITouch;

            if (touch != null)
            {
                startPoint = touch.LocationInView(this);
                SetNeedsDisplay();
            }

            StoreStartCoordiante();
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            if (!swiped)
            {
                co_ordinates.Add(new ScratchedPoints { startPoint = startPoint, endPoint = endPoint });
                SetNeedsDisplay();
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            var touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                endPoint = touch.LocationInView(this);
                if (!currentFrame.Contains(this.endPoint))
                {
                    return;
                }

                StoreEndCoordinate();
                swiped = true;
                co_ordinates.Add(new ScratchedPoints { startPoint = startPoint, endPoint = endPoint });
                startPoint = endPoint;
                SetNeedsDisplay();
            }
        }



        #region Calculations

        void DrawLineFrom(CGPoint fromPoint, CGPoint toPoint)
        {
            if (fromPoint.X == 0 && fromPoint.Y == 0 && toPoint.X == 0 && toPoint.Y == 0)
                return;

            context.SetLineWidth(40);
            context.MoveTo(fromPoint.X, fromPoint.Y);
            context.SetBlendMode(CGBlendMode.Clear);
            context.SetLineCap(CGLineCap.Round);
            context.AddLineToPoint(toPoint.X, toPoint.Y);
            context.StrokePath();
        }

        private void StoreStartCoordiante()
        {
            var temp_x = Convert.ToInt32(startPoint.X);
            var temp_y = Convert.ToInt32(startPoint.Y);

            if (temp_x < min_x)
            {
                min_x = temp_x;
            }

            if (temp_y < min_y)
            {
                min_y = temp_y;
            }
        }

        void Reset()
        {
            co_ordinates.Clear();
            min_x = 1000;
            max_x = 0;

            min_y = 1000;
            max_y = 0;
            this.SetNeedsDisplay();
        }

        private void CalculateUserScratchArea()
        {
            var scratchCardArea = this.currentFrame.Width * this.currentFrame.Height;

            var maxLengthOfScratchSurface = (max_y - min_y);
            var maxWidthOfScratchSurface = (max_x - min_x);

            var scratchedArea = maxWidthOfScratchSurface * maxLengthOfScratchSurface;
            if (scratchCardArea <= 0)
                return;

            var scratchPercentage = (scratchedArea / scratchCardArea) * 100;
            ScratchCompleted?.Invoke((int)scratchPercentage);
            //if (scratchPercentage >= ScrachRevealMinPercent && !IsCallbackCompleted)
            //{
            //    ScratchCompleted?.Invoke((int)scratchPercentage);
            //    IsCallbackCompleted = true;
            //}
        }

        private void StoreEndCoordinate()
        {
            var temp_x = Convert.ToInt32(endPoint.X);
            var temp_y = Convert.ToInt32(endPoint.Y);

            if (temp_x > max_x)
            {
                max_x = temp_x;
            }
            if (temp_y > max_y)
            {
                max_y = temp_y;
            }

            if (temp_x < min_x)
            {
                min_x = temp_x;
            }
            if (temp_y < min_y)
            {
                min_y = temp_y;
            }
        }
        #endregion
    }


    public class ScratchedPoints
    {
        public CGPoint startPoint { get; set; }
        public CGPoint endPoint { get; set; }
    }
}
