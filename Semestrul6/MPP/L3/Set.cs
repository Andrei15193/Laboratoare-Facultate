using System;
using System.Collections;
using System.Collections.Generic;
namespace L3
{
	internal class Set
		: IEnumerable<int>
	{
		/// <summary>
		/// Creates a new empty Set.
		/// </summary>
		public Set()
		{
		}
		/// <summary>
		/// Creates a new Set containg the specified elements.
		/// </summary>
		/// <param name="elements">
		/// The elements to be contained in the Set.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when elements is null.
		/// </exception>
		public Set(IEnumerable<int> elements)
		{
			if (elements == null)
				throw new ArgumentNullException("elements");

			foreach (int element in elements)
				Add(element);
		}
		/// <summary>
		/// Creates a new Set containg the specified elements.
		/// </summary>
		/// <param name="elements">
		/// The elements to be contained in the Set.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when elements is null.
		/// </exception>
		public Set(params int[] elements)
			: this((IEnumerable<int>)elements)
		{
		}

		/// <summary>
		/// Realizes the union between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in union.
		/// </param>
		/// <param name="second">
		/// The second Set to use in union.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains the elements of both sets.
		/// </returns>
		public static Set operator +(Set first, Set second)
		{
			if (first == null)
				if (second == null)
					return new Set();
				else
					return second;
			else
				if (second == null)
					return first;
				else
					return first.Union(first);
		}
		/// <summary>
		/// Realizes the union between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in union.
		/// </param>
		/// <param name="second">
		/// The second Set to use in union.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains the elements of both sets.
		/// </returns>
		public static Set operator +(object first, Set second)
		{
			return ((first as Set) + second);
		}
		/// <summary>
		/// Realizes the union between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in union.
		/// </param>
		/// <param name="second">
		/// The second Set to use in union.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains the elements of both sets.
		/// </returns>
		public static Set operator +(IEnumerable<int> first, Set second)
		{
			return ((first as Set) + second);
		}
		/// <summary>
		/// Realizes the union between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in union.
		/// </param>
		/// <param name="second">
		/// The second Set to use in union.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains the elements of both sets.
		/// </returns>
		public static Set operator +(Set first, object second)
		{
			return (first + (second as Set));
		}
		/// <summary>
		/// Realizes the union between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in union.
		/// </param>
		/// <param name="second">
		/// The second Set to use in union.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains the elements of both sets.
		/// </returns>
		public static Set operator +(Set first, IEnumerable<int> second)
		{
			return (first + (second as Set));
		}
		/// <summary>
		/// Realizes the intersection between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in intersection.
		/// </param>
		/// <param name="second">
		/// The second Set to use in intersection.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in both sets.
		/// </returns>
		public static Set operator -(Set first, Set second)
		{
			if (first == null)
				return new Set();
			else
				if (second == null)
					return first;
				else
					return first.Intersect(first);
		}
		/// <summary>
		/// Realizes the intersection between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in intersection.
		/// </param>
		/// <param name="second">
		/// The second Set to use in intersection.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in both sets.
		/// </returns>
		public static Set operator -(object first, Set second)
		{
			return ((first as Set) - second);
		}
		/// <summary>
		/// Realizes the intersection between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in intersection.
		/// </param>
		/// <param name="second">
		/// The second Set to use in intersection.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in both sets.
		/// </returns>
		public static Set operator -(IEnumerable<int> first, Set second)
		{
			return ((first as Set) - second);
		}
		/// <summary>
		/// Realizes the intersection between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in intersection.
		/// </param>
		/// <param name="second">
		/// The second Set to use in intersection.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in both sets.
		/// </returns>
		public static Set operator -(Set first, object second)
		{
			return (first - (second as Set));
		}
		/// <summary>
		/// Realizes the intersection between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in intersection.
		/// </param>
		/// <param name="second">
		/// The second Set to use in intersection.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in both sets.
		/// </returns>
		public static Set operator -(Set first, IEnumerable<int> second)
		{
			return (first - (second as Set));
		}
		/// <summary>
		/// Realizes the intersection between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in intersection.
		/// </param>
		/// <param name="second">
		/// The second Set to use in intersection.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in both sets.
		/// </returns>
		public static Set operator *(Set first, Set second)
		{
			if (first == null)
				if (second == null)
					return new Set();
				else
					return second;
			else
				if (second == null)
					return first;
				else
					return first.Union(first);
		}
		/// <summary>
		/// Realizes the difference between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in difference.
		/// </param>
		/// <param name="second">
		/// The second Set to use in difference.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in the first Set
		/// and not in the second Set.
		/// </returns>
		public static Set operator *(object first, Set second)
		{
			return ((first as Set) * second);
		}
		/// <summary>
		/// Realizes the difference between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in difference.
		/// </param>
		/// <param name="second">
		/// The second Set to use in difference.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in the first Set
		/// and not in the second Set.
		/// </returns>
		public static Set operator *(IEnumerable<int> first, Set second)
		{
			return ((first as Set) * second);
		}
		/// <summary>
		/// Realizes the difference between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in difference.
		/// </param>
		/// <param name="second">
		/// The second Set to use in difference.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in the first Set
		/// and not in the second Set.
		/// </returns>
		public static Set operator *(Set first, object second)
		{
			return (first * (second as Set));
		}
		/// <summary>
		/// Realizes the difference between two sets.
		/// </summary>
		/// <param name="first">
		/// The first Set to use in difference.
		/// </param>
		/// <param name="second">
		/// The second Set to use in difference.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are found in the first Set
		/// and not in the second Set.
		/// </returns>
		public static Set operator *(Set first, IEnumerable<int> second)
		{
			return (first * (second as Set));
		}

		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator ==(Set first, Set second)
		{
			return object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator ==(Set first, IEnumerable<int> second)
		{
			return object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator ==(Set first, object second)
		{
			return object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator ==(IEnumerable<int> first, Set second)
		{
			return object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator ==(object first, Set second)
		{
			return object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator !=(Set first, Set second)
		{
			return !object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator !=(Set first, IEnumerable<int> second)
		{
			return !object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator !=(Set first, object second)
		{
			return !object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator !=(IEnumerable<int> first, Set second)
		{
			return !object.Equals(first, second);
		}
		/// <summary>
		/// Determines whether the two Sets are equal.
		/// </summary>
		/// <param name="first">
		/// The first Set to check equality with.
		/// </param>
		/// <param name="second">
		/// The second Set to check equality with.</param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public static bool operator !=(object first, Set second)
		{
			return !object.Equals(first, second);
		}

		#region IEnumerable<int> Members
		/// <summary>
		/// Gets an enumerator that can enumerate all elements in the Set.
		/// </summary>
		/// <returns>
		/// Returns an <see cref="System.Collections.Generic.IEnumerator<int>"/>
		/// that enumerates all elements in the Set.
		/// </returns>
		public IEnumerator<int> GetEnumerator()
		{
			return new Enumerator(this);
		}
		#endregion
		#region IEnumerable Members
		/// <summary>
		/// Gets an enumerator that can enumerate all elements in the Set.
		/// </summary>
		/// <returns>
		/// Returns an <see cref="System.Collections.IEnumerator"/>
		/// that enumerates all elements in the Set.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
		/// <summary>
		/// Checks whether the current Set is equal to the specified one.
		/// </summary>
		/// <param name="obj">
		/// The Set to check equality with.
		/// </param>
		/// <returns>
		/// Returns true if the two Sets are equal, false otherwise.
		/// </returns>
		public override bool Equals(object obj)
		{
			Set other = (obj as Set);
			if (other == null || _cardinal != other._cardinal)
				return false;

			int elementIndex = 0;
			while (elementIndex < _cardinal && other.Contains(_elements[elementIndex]))
				elementIndex++;

			if (elementIndex <= _cardinal)
				return false;

			elementIndex = 0;
			while (elementIndex < other._cardinal && Contains(other._elements[elementIndex]))
				elementIndex++;

			return (elementIndex == _cardinal);
		}
		/// <summary>
		/// Gets the hash code for the current instance.
		/// </summary>
		/// <returns>
		/// Returns an <see cref="System.Int32"/> that represents the
		/// hash code for the current Set.
		/// </returns>
		public override int GetHashCode()
		{
			int hashCode = 0;

			foreach (int element in _elements)
				hashCode ^= element.GetHashCode();

			return hashCode;
		}
		/// <summary>
		/// Returns the string representation of the current instance.
		/// </summary>
		/// <returns>
		/// Returns a <see cref="System.String"/> which is the string
		/// representation of the current Set.
		/// </returns>
		public override string ToString()
		{
			return string.Format("{{ {0} }}", string.Join(", ", this));
		}
		/// <summary>
		/// Adds an element to the Set.
		/// </summary>
		/// <param name="element">
		/// The element to add.
		/// </param>
		/// <returns>
		/// Returns true if the element was successfully added
		/// (the element was not present in the set), false otherwise.
		/// </returns>
		public bool Add(int element)
		{
			if (Contains(element))
				return false;

			if (_cardinal == _elements.Length)
			{
				int[] newElementsArray = new int[_elements.Length * 2];
				_elements.CopyTo(newElementsArray, 0);
				_elements = newElementsArray;
			}
			_elements[_cardinal] = element;
			_cardinal++;
			_lastModificationTime = DateTime.Now;

			return true;
		}
		/// <summary>
		/// Removes an element from the Set.
		/// </summary>
		/// <param name="element">
		/// The element to remove.
		/// </param>
		/// <returns>
		/// Returns true if the element was successfully removed
		/// (the element was present in the set), false otherwise.
		/// </returns>
		public bool Remove(int element)
		{
			int elementIndex = 0;

			while (elementIndex < _cardinal && _elements[elementIndex] != elementIndex)
				elementIndex++;

			if (elementIndex == _cardinal)
				return false;

			for (int overridingElementIndex = elementIndex + 1; overridingElementIndex < _cardinal; overridingElementIndex++)
				_elements[overridingElementIndex - 1] = _elements[overridingElementIndex];
			_cardinal--;
			_lastModificationTime = DateTime.Now;

			return true;
		}
		/// <summary>
		/// Checks whether an element exists in the current Set.
		/// </summary>
		/// <param name="element">
		/// The element to check existence with.
		/// </param>
		/// <returns>
		/// Returns true if the element exists in the Set, false otherwise.
		/// </returns>
		public bool Contains(int element)
		{
			int elementIndex = 0;

			while (elementIndex < _cardinal && _elements[elementIndex] != element)
				elementIndex++;

			return (elementIndex < _cardinal);
		}
		/// <summary>
		/// Realizes the union of the current Set and the given one.
		/// </summary>
		/// <param name="set">
		/// The Set to realize union with.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains both the elements of the current Set,
		/// and the given one.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when set is null.
		/// </exception>
		public Set Union(Set set)
		{
			if (set == null)
				throw new ArgumentNullException("set");

			Set result = new Set(this);

			foreach (int element in set)
				result.Add(element);

			return result;
		}
		/// <summary>
		/// Realizes the intersection of the current Set and the given one.
		/// </summary>
		/// <param name="set">
		/// The Set to realize intersection with.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are present
		/// in the current Set and the specified one.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when set is null.
		/// </exception>
		public Set Intersect(Set set)
		{
			if (set == null)
				throw new ArgumentNullException("set");

			Set result = new Set();

			foreach (int element in _elements)
				if (set.Contains(element))
					result.Add(element);

			return result;
		}
		/// <summary>
		/// Realizes the difference between the current Set and the specified one.
		/// </summary>
		/// <param name="set">
		/// The Set to exclude.
		/// </param>
		/// <returns>
		/// Returns a new Set that contains only the elements that are present
		/// in the current Set and not in the specified one.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when set is null.
		/// </exception>
		public Set Difference(Set set)
		{
			if (set == null)
				throw new ArgumentNullException("set");

			Set result = new Set();

			foreach (int element in this)
				if (!set.Contains(element))
					result.Add(element);

			return result;
		}
		/// <summary>
		/// Returns the number of elements present in the set.
		/// </summary>
		public int Count
		{
			get
			{
				return _cardinal;
			}
		}

		private int _cardinal = 0;
		private int[] _elements = new int[8];
		private DateTime _lastModificationTime = DateTime.Now;

		private sealed class Enumerator
			: IEnumerator<int>
		{
			/// <summary>
			/// Creates a new enumerator for the given set.
			/// </summary>
			/// <param name="set">
			/// The set whos elements to enumerate.
			/// </param>
			/// <exception cref="System.ArgumentNullException">
			/// Thrown when set is null.
			/// </exception>
			public Enumerator(Set set)
			{
				if (set == null)
					throw new ArgumentNullException("set");

				_set = set;
				_setLastModificationTime = _set._lastModificationTime;
				Reset();
			}

			#region IEnumerator<int> Members
			/// <summary>
			/// Returns the current element.
			/// </summary>
			/// <exception cref="System.InvalidOperationException">
			/// Thrown when there is no element to return (the last call to MoveNext() returned false)
			/// or when the underlying Set that is being iterated has changed.
			/// </exception>
			public int Current
			{
				get
				{
					if (_index < 0 || _set._cardinal <= _index || _setLastModificationTime != _set._lastModificationTime)
						throw new InvalidOperationException();

					return _set._elements[_index];
				}
			}
			#endregion
			#region IDisposable Members
			/// <summary>
			/// Disposes the current instace.
			/// </summary>
			void IDisposable.Dispose()
			{
			}
			#endregion
			#region IEnumerator Members
			/// <summary>
			/// Returns the current element.
			/// </summary>
			/// <exception cref="System.InvalidOperationException">
			/// Thrown when there is no element to return (the last call to MoveNext() returned false)
			/// or when the underlying Set that is being iterated has changed.
			/// </exception>
			object IEnumerator.Current
			{
				get
				{
					return Current;
				}
			}
			/// <summary>
			/// Advances the enumerator on the next element.
			/// </summary>
			/// <returns>
			/// Returns true if there was a next element and the enumerator successfulyl
			/// advanced to it, false otherwise.
			/// </returns>
			/// <exception cref="System.InvalidOperationException">
			/// Thrown when the underlying Set that is being iterated has changed.
			/// </exception>
			public bool MoveNext()
			{
				if (_setLastModificationTime != _set._lastModificationTime)
					throw new InvalidOperationException();

				_index++;
				return (_index < _set.Count);
			}
			/// <summary>
			/// Resets the enumerator making it iterate all elements of the underlying
			/// Set again.
			/// </summary>
			public void Reset()
			{
				_index = -1;
			}
			#endregion

			private int _index;
			private readonly Set _set;
			private readonly DateTime _setLastModificationTime;
		}
	}
}