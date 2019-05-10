using Xunit;
using Moq;
using Depersonalizer.Common;

namespace Depersonalizer.Profile.Tests
{
	public class ReplacerChainTests
	{
		[Fact]
		public void Test_Last()
		{
			var mockReplacer1 = new Mock<IDataReplacer>();
			var mockReplacer2 = new Mock<IDataReplacer>();

			mockReplacer1.SetupGet(x => x.NextReplacer).Returns(mockReplacer2.Object);
			mockReplacer2.SetupGet(x => x.NextReplacer).Returns((IDataReplacer)null);

			var chain = new ReplacerChain();

			chain.Root = null;
			Assert.Null(chain.Last);

			chain.Root = mockReplacer1.Object;
			Assert.Equal(mockReplacer2.Object, chain.Last);
		}

		[Fact]
		public void Test_Add()
		{
			var mockReplacer1 = new Mock<IDataReplacer>();
			var mockReplacer2 = new Mock<IDataReplacer>();

			IDataReplacer next1 = null, next2 = null;

			mockReplacer1.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next1 = value);
			mockReplacer1.SetupGet(x => x.NextReplacer).Returns(() => { return next1; });

			mockReplacer2.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next2 = value);
			mockReplacer2.SetupGet(x => x.NextReplacer).Returns(() => { return next2; });

			var chain = new ReplacerChain();

			chain.Add(mockReplacer1.Object);
			Assert.Equal(mockReplacer1.Object, chain.Root);
			Assert.Null(next1);
			Assert.Null(next2);

			chain.Add(mockReplacer2.Object);
			Assert.Equal(mockReplacer1.Object, chain.Root);
			Assert.Equal(mockReplacer2.Object, next1);
			Assert.Null(next2);
		}

		[Fact]
		public void Test_GetNext()
		{
			var mockReplacer1 = new Mock<IDataReplacer>();
			var mockReplacer2 = new Mock<IDataReplacer>();

			mockReplacer1.SetupGet(x => x.NextReplacer).Returns(mockReplacer2.Object);
			mockReplacer2.SetupGet(x => x.NextReplacer).Returns((IDataReplacer)null);

			var chain = new ReplacerChain();
			chain.Root = mockReplacer1.Object;

			var next = chain.GetNext(null);
			Assert.Null(next);

			next = chain.GetNext(mockReplacer1.Object);
			Assert.Equal(mockReplacer2.Object, next);

			next = chain.GetNext(mockReplacer2.Object);
			Assert.Null(next);
		}

		[Fact]
		public void Test_GetPrevious()
		{
			var mockReplacer1 = new Mock<IDataReplacer>();
			var mockReplacer2 = new Mock<IDataReplacer>();

			mockReplacer1.SetupGet(x => x.NextReplacer).Returns(mockReplacer2.Object);
			mockReplacer2.SetupGet(x => x.NextReplacer).Returns((IDataReplacer)null);

			var chain = new ReplacerChain();
			chain.Root = mockReplacer1.Object;

			var prev = chain.GetPrevious(null);
			Assert.Null(prev);

			prev = chain.GetPrevious(mockReplacer1.Object);
			Assert.Null(prev);

			prev = chain.GetPrevious(mockReplacer2.Object);
			Assert.Equal(mockReplacer1.Object, prev);
		}

		[Fact]
		public void Test_Remove()
		{
			var mockReplacer1 = new Mock<IDataReplacer>();
			var mockReplacer2 = new Mock<IDataReplacer>();
			var mockReplacer3 = new Mock<IDataReplacer>();

			IDataReplacer next1 = mockReplacer2.Object, next2 = mockReplacer3.Object, next3 = null;

			mockReplacer1.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next1 = value);
			mockReplacer1.SetupGet(x => x.NextReplacer).Returns(() => { return next1; });

			mockReplacer2.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next2 = value);
			mockReplacer2.SetupGet(x => x.NextReplacer).Returns(() => { return next2; });

			mockReplacer3.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next3 = value);
			mockReplacer3.SetupGet(x => x.NextReplacer).Returns(() => { return next3; });

			var chain = new ReplacerChain();
			chain.Root = mockReplacer1.Object;

			chain.Remove(mockReplacer2.Object);
			Assert.Equal(mockReplacer1.Object, chain.Root);
			Assert.Equal(mockReplacer3.Object, next1);
			Assert.Null(next2);
			Assert.Null(next3);

			chain.Remove(mockReplacer1.Object);
			Assert.Equal(mockReplacer3.Object, chain.Root);
			Assert.Null(next1);
			Assert.Null(next2);
			Assert.Null(next3);

			chain.Remove(mockReplacer3.Object);
			Assert.Null(chain.Root);
			Assert.Null(next1);
			Assert.Null(next2);
			Assert.Null(next3);

			chain.Remove(mockReplacer3.Object);
			Assert.Null(chain.Root);
			Assert.Null(next3);
		}

		[Fact]
		public void Test_MoveDown()
		{
			var mockReplacer1 = new Mock<IDataReplacer>();
			var mockReplacer2 = new Mock<IDataReplacer>();
			var mockReplacer3 = new Mock<IDataReplacer>();

			IDataReplacer next1 = null, next2 = null, next3 = null;

			mockReplacer1.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next1 = value);
			mockReplacer1.SetupGet(x => x.NextReplacer).Returns(() => { return next1; });

			mockReplacer2.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next2 = value);
			mockReplacer2.SetupGet(x => x.NextReplacer).Returns(() => { return next2; });

			mockReplacer3.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next3 = value);
			mockReplacer3.SetupGet(x => x.NextReplacer).Returns(() => { return next3; });

			var chain = new ReplacerChain();
			chain.Root = mockReplacer1.Object;

			chain.MoveDown(mockReplacer1.Object);
			Assert.Equal(mockReplacer1.Object, chain.Root);
			Assert.Null(next1);
			Assert.Null(next2);
			Assert.Null(next3);

			next1 = mockReplacer2.Object;
			next2 = mockReplacer3.Object;

			chain.MoveDown(mockReplacer1.Object);
			Assert.Equal(mockReplacer2.Object, chain.Root);
			Assert.Equal(mockReplacer3.Object, next1);
			Assert.Equal(mockReplacer1.Object, next2);
			Assert.Null(next3);

			chain.MoveDown(mockReplacer1.Object);
			Assert.Equal(mockReplacer2.Object, chain.Root);
			Assert.Null(next1);
			Assert.Equal(mockReplacer3.Object, next2);
			Assert.Equal(mockReplacer1.Object, next3);

			chain.MoveDown(mockReplacer1.Object);
			Assert.Equal(mockReplacer2.Object, chain.Root);
			Assert.Null(next1);
			Assert.Equal(mockReplacer3.Object, next2);
			Assert.Equal(mockReplacer1.Object, next3);
		}

		[Fact]
		public void Test_MoveUp()
		{
			var mockReplacer1 = new Mock<IDataReplacer>();
			var mockReplacer2 = new Mock<IDataReplacer>();
			var mockReplacer3 = new Mock<IDataReplacer>();

			IDataReplacer next1 = null, next2 = null, next3 = null;

			mockReplacer1.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next1 = value);
			mockReplacer1.SetupGet(x => x.NextReplacer).Returns(() => { return next1; });

			mockReplacer2.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next2 = value);
			mockReplacer2.SetupGet(x => x.NextReplacer).Returns(() => { return next2; });

			mockReplacer3.SetupSet(x => x.NextReplacer = It.IsAny<IDataReplacer>()).Callback<IDataReplacer>(value => next3 = value);
			mockReplacer3.SetupGet(x => x.NextReplacer).Returns(() => { return next3; });

			var chain = new ReplacerChain();
			chain.Root = mockReplacer1.Object;

			chain.MoveUp(mockReplacer1.Object);
			Assert.Equal(mockReplacer1.Object, chain.Root);
			Assert.Null(next1);
			Assert.Null(next2);
			Assert.Null(next3);

			next1 = mockReplacer2.Object;
			next2 = mockReplacer3.Object;

			chain.MoveUp(mockReplacer3.Object);
			Assert.Equal(mockReplacer1.Object, chain.Root);
			Assert.Equal(mockReplacer3.Object, next1);
			Assert.Null(next2);
			Assert.Equal(mockReplacer2.Object, next3);

			chain.MoveUp(mockReplacer3.Object);
			Assert.Equal(mockReplacer3.Object, chain.Root);
			Assert.Equal(mockReplacer2.Object, next1);
			Assert.Null(next2);
			Assert.Equal(mockReplacer1.Object, next3);

			chain.MoveUp(mockReplacer3.Object);
			Assert.Equal(mockReplacer3.Object, chain.Root);
			Assert.Equal(mockReplacer2.Object, next1);
			Assert.Null(next2);
			Assert.Equal(mockReplacer1.Object, next3);
		}

		[Fact]
		public void Test_ToList()
		{
			var mockReplacer1 = new Mock<IDataReplacer>();
			var mockReplacer2 = new Mock<IDataReplacer>();

			mockReplacer1.SetupGet(x => x.NextReplacer).Returns(mockReplacer2.Object);
			mockReplacer2.SetupGet(x => x.NextReplacer).Returns((IDataReplacer)null);

			var chain = new ReplacerChain();
			chain.Root = null;

			var list = chain.ToList();
			Assert.Empty(list);

			chain.Root = mockReplacer1.Object;

			list = chain.ToList();
			Assert.Equal(2, list.Count);
			Assert.Equal(mockReplacer1.Object, list[0]);
			Assert.Equal(mockReplacer2.Object, list[1]);
		}
	}
}
