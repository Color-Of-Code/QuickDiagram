﻿using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Codartis.SoftVis.Util.UI.Wpf.Collections;
using Codartis.SoftVis.Util.UI.Wpf.ViewModels;

namespace Codartis.SoftVis.Util.UI.Wpf.Controls
{
    /// <summary>
    /// Presents a collection of view model items with animated position changes.
    /// Creates a container for each view model and that container creates the proper control corresponding to the view model.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TContentPresenter">The type of the content presenter.</typeparam>
    /// <remarks>
    /// In order to implement fade out animation when removing an item, the source collection is duplicated.
    /// The original collection is kept and the view model keeps manipulating that, 
    /// but beside that a second collection is created which is the presented collection.
    /// Modifications of the original collection are executed on the presented collection as well,
    /// but in the case of a removal the item gets a chance to perform some action (eg. fade out animation) 
    /// before its actual removal from the presented collection.
    /// </remarks>
    public abstract class AnimatedItemsControl<TViewModel, TContentPresenter> : ItemsControl
        where TViewModel : ViewModelBase
        where TContentPresenter : AnimatedContentPresenter, new()
    {
        private ThreadSafeObservableCollection<TViewModel> _originalItemsSource;
        private ThreadSafeObservableCollection<TViewModel> _presentedItemsSource;

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TContentPresenter();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TContentPresenter;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (ReferenceEquals(newValue, _presentedItemsSource))
                return;

            SetUpDuplicatedItemsSource((ThreadSafeObservableCollection<TViewModel>)newValue);
        }

        private void SetUpDuplicatedItemsSource(ThreadSafeObservableCollection<TViewModel> viewModels)
        {
            _originalItemsSource = viewModels;
            ((INotifyCollectionChanged)_originalItemsSource).CollectionChanged += OnOriginalCollectionChanged;

            _presentedItemsSource = new ThreadSafeObservableCollection<TViewModel>(_originalItemsSource);

            ItemsSource = _presentedItemsSource;
        }

        private void OnOriginalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItemsToPresentedCollection(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveItemsFromPresentedCollection(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    RemoveItemsFromPresentedCollection(_presentedItemsSource);
                    break;
                default:
                    throw new NotImplementedException($"NotifyCollectionChangedAction {e.Action} is not handled.");
            }
        }

        private void AddItemsToPresentedCollection(IList newItems)
        {
            foreach (var newItem in newItems)
                _presentedItemsSource.Add((TViewModel)newItem);
        }

        private void RemoveItemsFromPresentedCollection(IList oldItems)
        {
            foreach (var oldItem in oldItems.OfType<TViewModel>().ToList())
            {
                var container = ItemContainerGenerator.ContainerFromItem(oldItem) as TContentPresenter;
                container?.OnBeforeRemove(OnItemReadyToBeRemoved);
            }
        }

        private void OnItemReadyToBeRemoved(ViewModelBase viewModel)
        {
            _presentedItemsSource.Remove((TViewModel)viewModel);
        }
    }
}
