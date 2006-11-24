using System;
using System.Drawing;
using System.Collections.Generic;

namespace Netcode.Network.RSS.UI
{
	/// <summary>
	/// Encapsulates the rendering of a list of items.  Each item's description is shown in a list, and a single item is selected.
	/// </summary>
	/// <typeparam name="T">The type of item that this ItemListView will draw.</typeparam>
	public class ItemListView<T> : IDisposable where T : IItem
	{
		private string title;
		private int selectedIndex = 0;
		// The list of items to draw
		private IList<T> items;
		private int NumArticles { get { return items.Count; } }
		public T SelectedItem { get { return items[selectedIndex]; } }

		public void NextArticle()
		{
			if (selectedIndex < NumArticles - 1)
				selectedIndex++;
			else
				selectedIndex = 0;
		}

		public void PreviousArticle()
		{
			if (selectedIndex > 0)
				selectedIndex--;
			else
				selectedIndex = NumArticles - 1;
		}

		public ItemListView(string title, IList<T> items)
		{
			if (items == null)
				throw new ArgumentException("Items cannot be null", "items");

			this.items = items;
			this.title = title;
		}

		/// <summary>
		/// Dispose all disposable fields
		/// </summary>
		public void Dispose()
		{
		}
	}
}