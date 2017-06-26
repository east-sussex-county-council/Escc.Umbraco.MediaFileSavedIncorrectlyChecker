using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker.Tests
{
    [TestClass]
    public class UmbracoThumbnailFilterTests
    {
        [TestMethod]
        public void JpgThumbnailIsFiltered()
        {
            var files = new[] { "example.jpg", "example_thumb.jpg" };
            var filter = new UmbracoThumbnailFilter();

            var filteredList = filter.Filter(files);

            Assert.AreEqual(1, filteredList.Count());
        }

        [TestMethod]
        public void BigThumbnailOfJpgIsFiltered()
        {
            var files = new[] { "example.jpg", "example_big-thumb.jpg" };
            var filter = new UmbracoThumbnailFilter();

            var filteredList = filter.Filter(files);

            Assert.AreEqual(1, filteredList.Count());
        }

        [TestMethod]
        public void JpgThumbnailOfPngIsFiltered()
        {
            var files = new[] { "example.png", "example_thumb.jpg" };
            var filter = new UmbracoThumbnailFilter();

            var filteredList = filter.Filter(files);

            Assert.AreEqual(1, filteredList.Count());
        }

        [TestMethod]
        public void BigJpgThumbnailOfPngIsFiltered()
        {
            var files = new[] { "example.png", "example_big-thumb.jpg" };
            var filter = new UmbracoThumbnailFilter();

            var filteredList = filter.Filter(files);

            Assert.AreEqual(1, filteredList.Count());
        }

        [TestMethod]
        public void DifferentImageFilesAreNotThumbnails()
        {
            var files = new[] { "example.jpg", "example1.jpg" };
            var filter = new UmbracoThumbnailFilter();

            var filteredList = filter.Filter(files);

            Assert.AreEqual(2, filteredList.Count());
        }

        [TestMethod]
        public void DifferentDocumentFilesAreNotThumbnails()
        {
            var files = new[] { "example.doc", "example.pdf" };
            var filter = new UmbracoThumbnailFilter();

            var filteredList = filter.Filter(files);

            Assert.AreEqual(2, filteredList.Count());
        }
    }
}
