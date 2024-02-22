namespace Store.Tests
{
    public class BookTests
    {
        [Fact]
        public void IsIsbn_WithNull_ReturnsFalse()
        {
            bool actual = Book.IsIsbn(null);
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithBlankString_ReturnsFalse()
        {
            bool actual = Book.IsIsbn("   ");
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithInvalidIsbn_ReturnsFalse()
        {
            bool actual = Book.IsIsbn("ISBN 12312-312");
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithTrashStart_ReturnsFalse()
        {
            bool actual = Book.IsIsbn("fdsISBN 12312-31231");
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithValidIsbn10_ReturnsTrue()
        {
            bool actual = Book.IsIsbn("ISBN 12312-31231");
            Assert.True(actual);
        }

        [Fact]
        public void IsIsbn_WithValidIsbn13_ReturnsTrue()
        {
            bool actual = Book.IsIsbn("ISBN 12312-31231231");
            Assert.True(actual);
        }
    }
}