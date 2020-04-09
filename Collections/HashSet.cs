using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
	public class HashSet<T>
	{
		private int _capacity;
		private int _count;
		private int _lastIndex;
		private Item[] _items;
		private int[] _baskets;
		private double _loadCoeff;

		private struct Item
		{
			//public Item(int hashcode)
			//{

			//}

			public T Value { get; set; }
			public int HashCode { get; set; }
			public int NextIndex { get; set; }
			public int PrevIndex { get; set; }
		}

		/// <summary>
		/// Инициализирует пустой экземпляр класса.
		/// </summary>
		public HashSet()
		{
			_lastIndex = 1;
			_capacity = 10;
			_loadCoeff = 0.75;
			_baskets = new int[_capacity];
			_items = new Item[_capacity];
		}

		/// <summary>
		/// Инициализирует пустой экземпляр класса с заданным значением количества элементов.
		/// </summary>
		/// <param name="capacity">Начальный размер.</param>
		public HashSet(int capacity)
		{
			_capacity = capacity;
			_loadCoeff = 0.75;
			_lastIndex = 1;
			_baskets = new int[_capacity];
			_items = new Item[_capacity];
		}

		/// <summary>
		/// Инициализирует пустой экземпляр класса с заданным значением количества элементов и коэффициентом загрузки.
		/// </summary>
		/// <param name="capacity">Начальный размер.</param>
		/// <param name="capacity">Коэффициент загрузки.</param>
		public HashSet(int capacity, double loadCoeff)
		{
			_lastIndex = 1;
			_capacity = capacity;
			_baskets = new int[_capacity];
			_items = new Item[_capacity];
			_loadCoeff = loadCoeff;
		}

		/// <summary>
		///  Возвращает число элементов, содержащихся в наборе.
		/// </summary>
		public int Count
		{
			get
			{
				return _count;
			}
		}

		/// <summary>
		/// true, если набор пуст
		/// false, если в наборе содержатся элементы.
		/// </summary>
		/// <returns></returns>
		public bool IsEmpty() => _count == 0;

		/// <summary>
		///  Добавляет указанный элемент в набор.
		/// </summary>
		/// <param name="item">Элемент, добавляемый в набор.</param>
		/// <returns> true, если элемент добавлен в объект
		///false, если элемент уже присутствует в нем.</returns>
		public bool Add(T item)
		{
			int hashCode = ComputeHashCode(item);
			int indexInBasket = hashCode % (_capacity - 1);

			int itemIndex = _baskets[indexInBasket];
			while (itemIndex != 0)
			{
				if (_items[itemIndex].HashCode == hashCode)
				{
					return false;
				}
				itemIndex = _items[itemIndex].NextIndex;
			}

			AddNewItem(item, hashCode, indexInBasket);

			double threshold = _capacity * _loadCoeff;
			if (Count >= threshold)
			{
				Increase();
			}
			return true;
		}

		/// <summary>
		///  Удаляет все элементы из объекта/
		/// </summary>
		public void Clear()
		{
			Array.Clear(_items, 0, _items.Length);
			Array.Clear(_baskets, 0, _capacity);
			_count = 0;
			_lastIndex = 1;
		}

		/// <summary>
		///  Определяет, содержит ли объект указанный элемент.
		/// </summary>
		/// <param name="item">Элемент, который нужно найти в объекте.</param>
		/// <returns>Значение true, если объект содержит указанный элемент; в противном случае — значение false.</returns>
		public bool Contains(T item)
		{
			int hashCode = ComputeHashCode(item);
			int basketIndex = hashCode % (_capacity - 1);
			int index = _baskets[basketIndex];

			while (index != 0)
			{
				Item tempItem = _items[index];
				if (tempItem.HashCode == hashCode && tempItem.Value.Equals(item))
				{
					return true;
				}
				index = _items[index].NextIndex;
			}
			return false;
		}

		/// <summary>
		/// Удаляет указанный элемент из объекта/
		/// </summary>
		/// <param name="item">Подлежащий удалению элемент.</param>
		/// <returns>Значение true, если элемент был найден и удален; в противном случае — значение false.</returns>
		public bool Remove(T item)
		{
			int hashCode = ComputeHashCode(item);
			int indexInBasket = hashCode % (_capacity - 1);

			int itemIndex = _baskets[indexInBasket];

			while (itemIndex != 0)
			{
				if (_items[itemIndex].HashCode == hashCode)
				{
					Item itemToRemove = _items[itemIndex];
					int prevIndex = itemToRemove.PrevIndex;

					if (prevIndex == 0)
					{
						_baskets[indexInBasket] = itemToRemove.NextIndex;
					}
					else
					{
						Item prevItem = _items[prevIndex];
						prevItem.NextIndex = itemToRemove.NextIndex;
					}
					SetDefault(itemIndex);
					_count--;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Возвращает представление набора в виде массива.
		/// </summary>
		/// <returns>Массив, содержащий копии элементов набора.</returns>
		public T[] ToArray()
		{
			var result = new T[_count];
			int arrayIndex = 0;
			for (int i = 1; i < _lastIndex; i++)
			{
				Item item = _items[i];
				if (!IsDefaultItem(item))
				{
					result[arrayIndex] = item.Value;
					arrayIndex++;
				}
			}
			return result;
		}

		private void AddNewItem(T item, int hashCode, int index)
		{
			Item itemInBasket = _items[_baskets[index]];
			_items[_lastIndex] = new Item
			{
				Value = item,
				HashCode = hashCode,
				PrevIndex = _baskets[index]
			};
			itemInBasket.NextIndex = _lastIndex;
			_baskets[index] = _lastIndex;
			_lastIndex++;
			_count++;
		}

		private int ComputeHashCode(T item)
		{
			return item.GetHashCode() & int.MaxValue;
		}

		private void Increase()
		{
			_capacity *= 2;
			IncreaseItemsArray();
			IncreaseBasketsArray();
		}

		private void IncreaseBasketsArray()
		{
			var tempArray = new int[_capacity];

			for (int i = 1; i < _lastIndex; i++)
			{
				Item item = _items[i];
				int hashCode = ComputeHashCode(item.Value);
				int indexInBasket = hashCode % (_capacity - 1);
				tempArray[indexInBasket] = i + 1;
			}
			_baskets = tempArray;
		}

		private void IncreaseItemsArray()
		{
			var tempArray = new Item[_capacity];
			Array.Copy(_items, 0, tempArray, 0, _lastIndex);
			for (int i = 1; i < _lastIndex; i++)
			{
				tempArray[i].HashCode = ComputeHashCode(_items[i].Value);
			}
			_items = tempArray;
		}

		private bool IsDefaultItem(Item item)
		{
			return ((item.HashCode == 0 || item.HashCode == -1)
				&& item.NextIndex == 0
				&& item.PrevIndex == 0
				|| item.Value == default);
		}

		private void SetDefault(int itemIndex)
		{
			_items[itemIndex].HashCode = -1;
			_items[itemIndex].NextIndex = 0;
			_items[itemIndex].PrevIndex = 0;
			_items[itemIndex].Value = default;
		}
	}
}
