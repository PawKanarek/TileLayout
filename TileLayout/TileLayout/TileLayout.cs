using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TileLayout
{
    public class TileLayout : Layout<View>
    {
        private View coverImage;
        private View watchingProgress;
        private View gradient;
        private View channelIconGradient;
        private View channelIcon;
        private View coverPoster;
        private View extraImage;
        private View time;
        private View ageRating;
        private View playIcon;
        private View playIndicator;
        private View isWatched;
        private View currentProgress;
        private View recordingIcon;
        private View recordingBackground;
        private View favoriteIcon;
        private View favoriteBackground;
        private View title;
        private View description;

        public View CoverImage { get => this.coverImage; set => this.SetAndUpdateChildren(ref this.coverImage, value); }
        public View WatchingProgress { get => this.watchingProgress; set => this.SetAndUpdateChildren(ref this.watchingProgress, value); }
        public View Gradient { get => this.gradient; set => this.SetAndUpdateChildren(ref this.gradient, value); }
        public View ChannelIconGradient { get => this.channelIconGradient; set => this.SetAndUpdateChildren(ref this.channelIconGradient, value); }
        public View ChannelIcon { get => this.channelIcon; set => this.SetAndUpdateChildren(ref this.channelIcon, value); }
        public View CoverPoster { get => this.coverPoster; set => this.SetAndUpdateChildren(ref this.coverPoster, value); }
        public View ExtraImage { get => this.extraImage; set => this.SetAndUpdateChildren(ref this.extraImage, value); }
        public View Time { get => this.time; set => this.SetAndUpdateChildren(ref this.time, value); }
        public View AgeRating { get => this.ageRating; set => this.SetAndUpdateChildren(ref this.ageRating, value); }
        public View PlayIcon { get => this.playIcon; set => this.SetAndUpdateChildren(ref this.playIcon, value); }
        public View PlayIndicator { get => this.playIndicator; set => this.SetAndUpdateChildren(ref this.playIndicator, value); }
        public View IsWatched { get => this.isWatched; set => this.SetAndUpdateChildren(ref this.isWatched, value); }
        public View CurrentProgress { get => this.currentProgress; set => this.SetAndUpdateChildren(ref this.currentProgress, value); }
        public View RecordingIcon { get => this.recordingIcon; set => this.SetAndUpdateChildren(ref this.recordingIcon, value); }
        public View RecordingBackground { get => this.recordingBackground; set => this.SetAndUpdateChildren(ref this.recordingBackground, value); }
        public View FavoriteIcon { get => this.favoriteIcon; set => this.SetAndUpdateChildren(ref this.favoriteIcon, value); }
        public View FavoriteBackground { get => this.favoriteBackground; set => this.SetAndUpdateChildren(ref this.favoriteBackground, value); }
        public View Title { get => this.title; set => this.SetAndUpdateChildren(ref this.title, value); }
        public View Description { get => this.description; set => this.SetAndUpdateChildren(ref this.description, value); }

        private void SetAndUpdateChildren(ref View field, View value)
        {
            if ((value == null || field != value) && field != null && this.Children.Contains(field))
            {
                this.Children.Remove(field);
            }

            field = value;

            if (field != null)
            {
                this.Children.Add(field);
            }
        }

        private readonly Dictionary<Size, LayoutData> layoutDataCache = new Dictionary<Size, LayoutData>();

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            LayoutData layoutData = this.GetLayoutData(widthConstraint, heightConstraint);
            if (layoutData.VisibleChildCount == 0)
            {
                return new SizeRequest();
            }

            var totalSize = new Size(layoutData.CellSize.Width * layoutData.Columns, layoutData.CellSize.Height * layoutData.Rows);

            return new SizeRequest(totalSize);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            LayoutData layoutData = this.GetLayoutData(width, height);

            if (layoutData.VisibleChildCount == 0)
            {
                return;
            }

            var startLocation = new Point(x, y);
            var constrainSize = new Size(width, height);

            if (CanLayout(this.CoverImage))
            {
                LayoutChildIntoBoundingRegion(this.CoverImage, new Rectangle(startLocation, new Size(width, this.CoverImage.HeightRequest)));
            }
            var coverImageHeigt = this.CoverImage?.HeightRequest ?? 0;

            if (CanLayout(this.WatchingProgress))
            {
                LayoutChildIntoBoundingRegion(this.WatchingProgress, new Rectangle(startLocation, new Size(width, this.WatchingProgress.HeightRequest)));
            }

            if (CanLayout(this.Gradient))
            {
                LayoutChildIntoBoundingRegion(this.Gradient, new Rectangle(startLocation, new Size(width, this.Gradient.HeightRequest > 0 ? this.Gradient.HeightRequest : 50)));
            }

            if (CanLayout(this.ChannelIconGradient))
            {
                LayoutChildIntoBoundingRegion(this.ChannelIconGradient, new Rectangle(startLocation, new Size(this.ChannelIconGradient.WidthRequest, this.ChannelIconGradient.HeightRequest)));
            }

            if (CanLayout(this.ChannelIcon))
            {
                LayoutChildIntoBoundingRegion(this.ChannelIcon, new Rectangle(new Point(x + 8, y + 8), new Size(this.ChannelIcon.WidthRequest, this.ChannelIcon.HeightRequest)));
            }

            if (CanLayout(this.CoverPoster))
            {
                LayoutChildIntoBoundingRegion(this.CoverPoster, new Rectangle(new Point(x + 10, y + 5), new Size(this.CoverPoster.WidthRequest, this.CoverPoster.HeightRequest)));
            }

            if (CanLayout(this.ExtraImage))
            {
                LayoutChildIntoBoundingRegion(this.ExtraImage, new Rectangle(startLocation, new Size(this.ExtraImage.WidthRequest, this.ExtraImage.HeightRequest)));
            }

            if (CanLayout(this.Time))
            {
                LayoutChildIntoBoundingRegion(this.Time, new Rectangle(new Point(x, coverImageHeigt - this.Time.HeightRequest), new Size(this.Time.WidthRequest, this.Time.HeightRequest)));
            }

            if (CanLayout(this.AgeRating))
            {
                LayoutChildIntoBoundingRegion(this.AgeRating, new Rectangle(new Point(this.Time.WidthRequest, coverImageHeigt - this.Time.HeightRequest), new Size(this.AgeRating.WidthRequest, this.AgeRating.HeightRequest)));
            }

            var playIconSize = new Size(48, 48);
            var playIconLocation = new Point(playIconSize.Width + 8, coverImageHeigt - playIconSize.Height - 8);
            if (CanLayout(this.PlayIcon))
            {
                LayoutChildIntoBoundingRegion(this.PlayIcon, new Rectangle(playIconLocation, playIconSize));
            }

            if (CanLayout(this.PlayIndicator))
            {
                LayoutChildIntoBoundingRegion(this.PlayIndicator, new Rectangle(playIconLocation, playIconSize));
            }

            if (CanLayout(this.IsWatched))
            {
                LayoutChildIntoBoundingRegion(this.IsWatched, new Rectangle(playIconLocation, playIconSize));
            }

            if (CanLayout(this.CurrentProgress))
            {
                LayoutChildIntoBoundingRegion(this.CurrentProgress, new Rectangle(new Point(x, y + this.CoverImage.HeightRequest), new Size(width, this.CurrentProgress.HeightRequest)));
            }

            var recordingSize = new Size(48, 48);
            var recordingLocation = new Point(width - recordingSize.Width - 8, recordingSize.Height - 8);
            if (CanLayout(this.RecordingBackground))
            {
                LayoutChildIntoBoundingRegion(this.RecordingBackground, new Rectangle(recordingLocation, recordingSize));
            }

            if (CanLayout(this.RecordingIcon))
            {
                LayoutChildIntoBoundingRegion(this.RecordingIcon, new Rectangle(recordingLocation, recordingSize));
            }

            if (CanLayout(this.FavoriteBackground))
            {
                LayoutChildIntoBoundingRegion(this.FavoriteBackground, new Rectangle(recordingLocation, recordingSize));
            }

            if (CanLayout(this.FavoriteIcon))
            {
                LayoutChildIntoBoundingRegion(this.FavoriteIcon, new Rectangle(recordingLocation, recordingSize));
            }

            if (CanLayout(this.Title))
            {
                LayoutChildIntoBoundingRegion(this.Title, new Rectangle(new Point(x, y + this.CoverImage.HeightRequest), new Size(this.Title.Width, this.Title.Height)));
            }

            if (CanLayout(this.Description))
            {
                LayoutChildIntoBoundingRegion(this.Description, new Rectangle(new Point(x, y + this.CoverImage.HeightRequest + this.Title.Height), new Size(width, height)));
            }

            bool CanLayout(View view)
            {
                return view != null && view.IsVisible;
            }
        }

        private LayoutData GetLayoutData(double width, double height)
        {
            var size = new Size(width, height);

            // Check if cached information is available.
            if (this.layoutDataCache.ContainsKey(size))
            {
                return this.layoutDataCache[size];
            }

            var visibleChildCount = 0;
            var maxChildSize = new Size();
            var rows = 0;
            var columns = 0;
            var layoutData = new LayoutData();

            // Enumerate through all the children.
            foreach (View child in this.Children)
            {
                // Skip invisible children.
                if (!child.IsVisible)
                {
                    continue;
                }

                // Count the visible children.
                visibleChildCount++;

                // Get the child's requested size.
                SizeRequest childSizeRequest = child.Measure(double.PositiveInfinity, double.PositiveInfinity);

                // Accumulate the maximum child size.
                maxChildSize.Width = Math.Max(maxChildSize.Width, childSizeRequest.Request.Width);
                maxChildSize.Height = Math.Max(maxChildSize.Height, childSizeRequest.Request.Height);
            }

            if (visibleChildCount != 0)
            {
                // Calculate the number of rows and columns.
                if (double.IsPositiveInfinity(width))
                {
                    columns = visibleChildCount;
                    rows = 1;
                }
                else
                {
                    columns = (int)((width) / (maxChildSize.Width));
                    columns = Math.Max(1, columns);
                    rows = (visibleChildCount + columns - 1) / columns;
                }

                // Now maximize the cell size based on the layout size.
                var cellSize = new Size();

                if (double.IsPositiveInfinity(width))
                {
                    cellSize.Width = maxChildSize.Width;
                }
                else
                {
                    cellSize.Width = (width) / columns;
                }

                if (double.IsPositiveInfinity(height))
                {
                    cellSize.Height = maxChildSize.Height;
                }
                else
                {
                    cellSize.Height = (height) / rows;
                }

                layoutData = new LayoutData(visibleChildCount, cellSize, rows, columns);
            }

            this.layoutDataCache.Add(size, layoutData);
            return layoutData;
        }

        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();

            // Discard all layout information for children added or removed.
            this.layoutDataCache.Clear();
        }

        protected override void OnChildMeasureInvalidated()
        {
            base.OnChildMeasureInvalidated();

            // Discard all layout information for child size changed.
            this.layoutDataCache.Clear();
        }
    }

    internal struct LayoutData
    {
        public int VisibleChildCount { get; private set; }

        public Size CellSize { get; private set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public LayoutData(int visibleChildCount, Size cellSize, int rows, int columns)
        {
            this.VisibleChildCount = visibleChildCount;
            this.CellSize = cellSize;
            this.Rows = rows;
            this.Columns = columns;
        }
    }

}
