using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TileLayout
{
    public class TileLayout : Layout<View>
    {
        public View CoverImage { get => this.coverImage; set { this.coverImage = value; this.Children.Add(this.coverImage); } }
        public View CoverPoster { get => coverPoster; set { coverPoster = value; this.Children.Add(this.coverPoster); } }
        public View Title { get => title; set { title = value; this.Children.Add(this.title); } }
        public View Description { get => description; set { description = value; this.Children.Add(this.description); } }

        private readonly Dictionary<Size, LayoutData> layoutDataCache = new Dictionary<Size, LayoutData>();
        private View coverImage;
        private View coverPoster;
        private View title;
        private View description;

        public void InvalidateTile()
        {
            this.InvalidateLayout();
        }

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


            if (this.CoverImage != null && this.CoverImage.IsVisible)
            {
                LayoutChildIntoBoundingRegion(this.CoverImage, new Rectangle(new Point(x, y), new Size(this.CoverImage.WidthRequest, this.CoverImage.HeightRequest)));
            }

            if (this.CoverPoster != null && this.CoverPoster.IsVisible)
            {
                LayoutChildIntoBoundingRegion(this.CoverPoster, new Rectangle(new Point(x + 50, y + 50), new Size(this.CoverPoster.WidthRequest, this.CoverPoster.HeightRequest)));
            }

            if (this.Title != null && this.Title.IsVisible)
            {
                LayoutChildIntoBoundingRegion(this.Title, new Rectangle(new Point(x, y + this.CoverImage.HeightRequest), new Size(width, height)));
            }

            if (this.Description != null && this.Description.IsVisible)
            {
                LayoutChildIntoBoundingRegion(this.Description, new Rectangle(new Point(x, y + this.CoverImage.HeightRequest + 50), new Size(width, height)));
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
