using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Collections.Tests
{
	[TestClass]
	public class HashSetTest
	{
		[TestMethod]
		public void Add_IfNoExists_Returns_True()
		{
			int count = 4;
			var hs = new HashSet<string>();
			InitStringHashSet(hs, count);
			bool addResult = hs.Add("5");

			var resultCount = hs.Count;
			Assert.IsTrue(addResult);
			Assert.AreEqual(resultCount, count + 1);
		}

		[TestMethod]
		public void Add_IfExists_Returns_False()
		{
			int count = 4;
			var hs = new HashSet<string>();
			InitStringHashSet(hs, count);

			bool addResult = hs.Add("1");
			var resultCount = hs.Count;

			Assert.IsFalse(addResult);
			Assert.AreEqual(resultCount, count);
		}

		[TestMethod]
		public void Clear_WithItems_Returns_0()
		{
			int count = 4;
			var listToInit = new List<string> { "1", "2", "3", "4" };
			var hs = new HashSet<string>();
			InitStringHashSet(hs, count);
			hs.Clear();

			var resultCount = hs.Count;
			Assert.AreEqual(resultCount, 0);
		}

		[TestMethod]
		public void Clear_Empty_Returns_0()
		{
			var hs = new HashSet<string>();
			hs.Clear();
			Assert.AreEqual(hs.Count, 0);
		}

		[TestMethod]
		public void Contains_IfNoPresent_Returns_False()
		{
			int count = 4;
			var hs = new HashSet<string>();
			InitStringHashSet(hs, count);

			bool result = hs.Contains("0");
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void Contains_IfPresent_Returns_True()
		{
			int count = 4;
			var hs = new HashSet<string>();
			InitStringHashSet(hs, count);

			bool result = hs.Contains("1");
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Remove_IfPresent_Returns_True()
		{
			int count = 4;
			var hs = new HashSet<string>();
			InitStringHashSet(hs, count);
			bool result = hs.Remove("1");
			var exprectedArray = new string[3] { "2", "3", "4" };

			Assert.IsTrue(result);
			CollectionAssert.AreEqual(hs.ToArray(), exprectedArray);
		}

		[TestMethod]
		public void Remove_IfNoPresent_Returns_False()
		{
			int count = 4;
			var hs = new HashSet<string>();
			InitStringHashSet(hs, count);
			bool result = hs.Remove("0");
			var exprectedArray = new string[4] {"1", "2", "3", "4" };

			Assert.IsFalse(result);
			CollectionAssert.AreEqual(hs.ToArray(), exprectedArray);
		}

		[TestMethod]
		public void ToArray_NotEmpty_Returns_InitialArray()
		{
			int count = 4;
			var hs = new HashSet<string>();
			InitStringHashSet(hs, count);
			var result = hs.ToArray();
			var exprectedArray = new string[4] { "1", "2", "3", "4" };

			CollectionAssert.AreEqual(result, exprectedArray);
		}

		[TestMethod]
		public void ToArray_Empty_Returns_EmptyArray()
		{
			var hs = new HashSet<string>();
			var result = hs.ToArray();

			CollectionAssert.AreEqual(result, new string[0]);
		}

		private void InitStringHashSet(HashSet<string> hs, int count)
		{
			for (int i = 0; i < count; i++)
			{
				hs.Add((i + 1).ToString());
			};
		}
	}
}
