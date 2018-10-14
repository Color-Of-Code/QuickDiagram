﻿using System;
using System.Windows;
using System.Windows.Input;

namespace Codartis.SoftVis.UI.Wpf.Behaviors
{
    /// <summary>
    /// Subscribes to the associated object's mouse events and translates them to pan and zoom commands.
    /// Captures the mouse while panning.
    /// </summary>
    /// <remarks>
    /// A mouse pan gesture is when the left mouse button is kept down and the mouse is moved.
    /// A mouse zoom gesture is turning the mouse wheel.
    /// </remarks>
    internal class MousePanAndZoomBehavior : PanAndZoomBehaviorBase
    {
        private Point _lastMousePosition;
        private bool _isPreparedForPanning;
        private bool _isPanning;
        private Cursor _cursorBeforePanning;

        public static readonly DependencyProperty PanCursorProperty =
            DependencyProperty.Register("PanCursor", typeof(Cursor), typeof(MousePanAndZoomBehavior),
                new PropertyMetadata(Cursors.Hand));

        public static readonly DependencyProperty ZoomAmountPerWheelClickProperty =
            DependencyProperty.Register("ZoomAmountPerWheelClick", typeof(double), typeof(MousePanAndZoomBehavior),
                new PropertyMetadata(1d));

        public Cursor PanCursor
        {
            get { return (Cursor)GetValue(PanCursorProperty); }
            set { SetValue(PanCursorProperty, value); }
        }

        public double ZoomAmountPerWheelClick
        {
            get { return (double)GetValue(ZoomAmountPerWheelClickProperty); }
            set { SetValue(ZoomAmountPerWheelClickProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            // Handled MouseMove events must be captured too otherwise focus tracking interferes with panning.
            // (Focus tracking stops mouse move event bubbling when moving inside a node.)
            AssociatedObject.AddHandler(UIElement.MouseMoveEvent, (MouseEventHandler)OnMouseMove, handledEventsToo: true);
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            AssociatedObject.LostMouseCapture += OnLostMouseCapture;
            AssociatedObject.MouseWheel += OnMouseWheel;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.RemoveHandler(UIElement.MouseMoveEvent, (MouseEventHandler)OnMouseMove);
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            AssociatedObject.LostMouseCapture -= OnLostMouseCapture;
            AssociatedObject.MouseWheel -= OnMouseWheel;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Don't start panning yet, just after the mouse was moved while holding down the mouse button.
            _isPreparedForPanning = true;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isPreparedForPanning = false;
            StopPanning();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            // Start panning only if the mouse button was already pushed but the panning has not started yet.
            if (_isPreparedForPanning && !_isPanning)
                StartPanning();

            var position = e.GetPosition(AssociatedObject);
            if (_isPanning && _lastMousePosition != position)
            {
                var panVector = position - _lastMousePosition;
                PanCommand?.Execute(panVector);
            }

            _lastMousePosition = position;
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            _isPanning = false;
            AssociatedObject.Cursor = _cursorBeforePanning;
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var zoomDirection = e.Delta < 0 ? ZoomDirection.Out : ZoomDirection.In;
            if (!IsZoomLimitReached(zoomDirection))
            {
                var zoomAmount = Math.Abs(e.Delta / Mouse.MouseWheelDeltaForOneLine) * ZoomAmountPerWheelClick;
                Zoom(zoomDirection, zoomAmount, e.GetPosition(AssociatedObject));
            }
        }

        private void StartPanning()
        {
            if (!_isPanning)
            {
                _isPanning = true;
                _cursorBeforePanning = AssociatedObject.Cursor;
                AssociatedObject.Cursor = PanCursor;
                Mouse.Capture(AssociatedObject);
            }
        }

        private void StopPanning()
        {
            if (_isPanning)
            {
                _isPanning = false;
                AssociatedObject.Cursor = _cursorBeforePanning;
                Mouse.Capture(null);
            }
        }
    }
}
