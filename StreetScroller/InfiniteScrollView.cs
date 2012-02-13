using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace StreetScroller
{
	public class InfiniteScrollView : UIScrollView
	{
		
		List<UILabel> visibleLabels;
		UIView         labelContainerView;
		
		public InfiniteScrollView (RectangleF _viewrect):base(_viewrect)
		{
			this.ContentSize = new SizeF (5000, this.Frame.Size.Height);
        
			visibleLabels = new List<UILabel> ();
        
			labelContainerView = new UIView ();
			labelContainerView.Frame = new RectangleF (0, 0, this.ContentSize.Width, this.ContentSize.Height / 2);
			this.AddSubview (labelContainerView);

			labelContainerView.UserInteractionEnabled = false;
			this.AutoresizingMask = UIViewAutoresizing.All;
        
			// hide horizontal scroll indicator so our recentering trick is not revealed
			this.ShowsHorizontalScrollIndicator = false;
			this.ShowsVerticalScrollIndicator = false;
			
		}
		
		
		
		// recenter content periodically to achieve impression of infinite scrolling
		private void RecenterIfNecessary ()
		{
			PointF currentOffset = this.ContentOffset;
			float contentWidth = this.ContentSize.Width;
			float centerOffsetX = (contentWidth - this.Bounds.Width) / 2.0f;
			float distanceFromCenter = Math.Abs (currentOffset.X - centerOffsetX);
    
			if (distanceFromCenter > (contentWidth / 4.0f)) {
				this.ContentOffset = new PointF (centerOffsetX, currentOffset.Y);
        
				// move content by the same amount so it appears to stay still
				foreach (UILabel label in visibleLabels) {
					PointF center = labelContainerView.ConvertPointToView (label.Center, this);
					center.X += (centerOffsetX - currentOffset.X);
					label.Center = this.ConvertPointToView (center, labelContainerView);
				}
			}
			
		}
		
		
		
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			
			this.RecenterIfNecessary ();
			
			// tile content in visible bounds
			RectangleF visibleBounds = this.ConvertRectToView (this.Bounds, labelContainerView);
			float minimumVisibleX = visibleBounds.GetMinX ();
			float maximumVisibleX = visibleBounds.GetMaxX ();
    
			this.TileLabelsFromMinX (minimumVisibleX, maximumVisibleX);
			
		}
	
		private UILabel InsertLabel ()
		{
			UILabel label = new UILabel (new RectangleF (0, 0, 500, 80));
			label.Lines = 3;
			label.Text = "1024 Block Street\nShaffer, CA\n95014";
			labelContainerView.AddSubview (label);

			return label;
		}
		
		private float PlaceNewLabelOnRight (float rightEdge)
		{
			UILabel label = this.InsertLabel ();
			visibleLabels.Add (label); // add rightmost label at the end of the array
    
			RectangleF frame = label.Frame;
			frame.X = rightEdge;
			frame.Y = labelContainerView.Bounds.Height - frame.Size.Height;
			label.Frame = frame;
        
			return frame.GetMaxX ();
		}
		
		private float PlaceNewLabelOnLeft (float leftEdge)
		{
			UILabel label = this.InsertLabel ();
			visibleLabels.Insert (0, label); // add leftmost label at the beginning of the array
    
			RectangleF frame = label.Frame;
			frame.X = leftEdge - frame.Size.Width;
			frame.Y = labelContainerView.Bounds.Height - frame.Size.Height;
			label.Frame = frame;
    
			return frame.GetMinX ();
		}
		
		private void TileLabelsFromMinX (float minimumVisibleX, float maximumVisibleX)
		{
			// the upcoming tiling logic depends on there already being at least one label in the visibleLabels array, so
			// to kick off the tiling we need to make sure there's at least one label
			if (visibleLabels.Count == 0) {
				this.PlaceNewLabelOnRight (minimumVisibleX);
			}
    
			// add labels that are missing on right side
			UILabel lastLabel = visibleLabels [visibleLabels.Count - 1];
			float rightEdge = lastLabel.Frame.GetMaxX ();
    
			while (rightEdge < maximumVisibleX) {
				rightEdge = this.PlaceNewLabelOnRight (rightEdge);
			}
    
			// add labels that are missing on left side
			UILabel firstLabel = visibleLabels [0];
			float leftEdge = firstLabel.Frame.GetMinX ();
			while (leftEdge > minimumVisibleX) {
				leftEdge = this.PlaceNewLabelOnLeft (leftEdge);
			}
    
			// remove labels that have fallen off right edge
			lastLabel = visibleLabels [visibleLabels.Count - 1];
			while (lastLabel.Frame.X > maximumVisibleX) {
				lastLabel.RemoveFromSuperview ();
				visibleLabels.RemoveAt (visibleLabels.Count - 1);
				lastLabel = visibleLabels [visibleLabels.Count - 1];
			}
    
			// remove labels that have fallen off left edge
			firstLabel = visibleLabels [0];
			while (firstLabel.Frame.GetMaxX() < minimumVisibleX) {
				firstLabel.RemoveFromSuperview ();
				visibleLabels.RemoveAt (0);
				firstLabel = visibleLabels [0];
			}
		}

		
	}
}

