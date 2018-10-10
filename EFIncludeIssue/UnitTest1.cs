using System;
using System.Data.Entity;
using System.Linq;
using EFIncludeIssue.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFIncludeIssue
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllDocuments()
        {
            using (var ctx = new TestDbContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var Docs = ctx.Documents
                    .Include(d => d.MetaInfo) // Load all the metaInfo for each object
                    .ToList(); // Actualize the collection

                Assert.AreEqual(3, Docs.Count);

                foreach (var d in Docs)
                {
                    Assert.IsTrue(d.MetaInfo.Any(m => m.Name == "Author"), "All documents must have at least one Author");
                    Assert.IsTrue(d.MetaInfo.Any(m => m.Name == "Created On"), "All documents must have a Created On meta value");
                }
            }
        }

        [TestMethod]
        public void GetAllDocumentsByAuthor()
        {
            using (var ctx = new TestDbContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var DocsByCreator = ctx.Documents
                    .Include(d => d.MetaInfo) // Load all the metaInfo for each object
                    .SelectMany(d => d.MetaInfo.Where(m => m.Name == "Author") // For each Author
                        .Select(m => new { Doc = d, Creator = m })) // Create an object with the Author and the Document they authored.
                    .ToList(); // Actualize the collection

                Assert.AreEqual(5, DocsByCreator.Count);

                foreach (var dc in DocsByCreator)
                {
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Author"), "All documents must have at least one Author");
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Created On"), "All documents must have a Created On meta value");
                }
            }
        }

        [TestMethod]
        public void GetAllDocumentsByAuthorEarlyActualize()
        {
            using (var ctx = new TestDbContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var DocsByCreator = ctx.Documents
                    .Include(d => d.MetaInfo) // Load all the metaInfo for each object
                    .ToList() // Actualize the whole database
                    .SelectMany(d => d.MetaInfo.Where(m => m.Name == "Author") // For each Author
                        .Select(m => new { Doc = d, Creator = m })) // Create an object with the Author and the Document they authored.
                    .ToList(); // Actualize the collection

                Assert.AreEqual(5, DocsByCreator.Count);

                foreach (var dc in DocsByCreator)
                {
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Author"), "All documents must have at least one Author");
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Created On"), "All documents must have a Created On meta value");
                }
            }
        }

        [TestMethod]
        public void GetAllDocumentsByAuthorNested()
        {
            using (var ctx = new TestDbContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var DocsByCreator = ctx.Documents
                    .Include(d => d.MetaInfo.Select(m => m.Document.MetaInfo)) // Load all the metaInfo for each object
                    .SelectMany(d => d.MetaInfo.Where(m => m.Name == "Author")) // Get the author Values
                    .Select(m => new { Doc = m.Document, Creator = m })
                    .ToList(); // Actualize the collection

                Assert.AreEqual(5, DocsByCreator.Count);

                foreach (var dc in DocsByCreator)
                {
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Author"), "All documents must have at least one Author");
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Created On"), "All documents must have a Created On meta value");
                }
            }
        }

        [TestMethod]
        public void GetAllDocumentsByAuthorNestedEarlyActualize()
        {
            using (var ctx = new TestDbContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var DocsByCreator = ctx.Documents
                    .Include(d => d.MetaInfo.Select(m => m.Document.MetaInfo)) // Load all the metaInfo for each object
                    .ToList() // Actualize the whole databse
                    .SelectMany(d => d.MetaInfo.Where(m => m.Name == "Author")) // Get the author Values
                    .Select(m => new { Doc = m.Document, Creator = m })
                    .ToList(); // Actualize the collection

                Assert.AreEqual(5, DocsByCreator.Count);

                foreach (var dc in DocsByCreator)
                {
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Author"), "All documents must have at least one Author");
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Created On"), "All documents must have a Created On meta value");
                }
            }
        }

        [TestMethod]
        public void GetAllMetaAuthorsPlusDocuments()
        {
            using (var ctx = new TestDbContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var DocsByCreator = ctx.Meta
                    .Where(m => m.Name == "Author")
                    .Include(m => m.Document.MetaInfo) // Load all the metaInfo for Document
                    .Select(m => new { Doc = m.Document, Creator = m })
                    .ToList(); // Actualize the collection

                Assert.AreEqual(5, DocsByCreator.Count);

                foreach (var dc in DocsByCreator)
                {
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Author"), "All documents must have at least one Author");
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Created On"), "All documents must have a Created On meta value");
                }
            }
        }

        [TestMethod]
        public void GetAllMetaAuthorsPlusDocumentsEarlyActualize()
        {
            using (var ctx = new TestDbContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var DocsByCreator = ctx.Meta
                    .Include(m => m.Document.MetaInfo) // Load all the metaInfo for each object
                    .Select(m => new { Doc = m.Document, Creator = m })
                    .ToList() // Actualize the Database
                    .Where(m => m.Creator.Name == "Author")
                    .ToList(); // Actualize the collection

                Assert.AreEqual(5, DocsByCreator.Count);

                foreach (var dc in DocsByCreator)
                {
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Author"), "All documents must have at least one Author");
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Created On"), "All documents must have a Created On meta value");
                }
            }
        }

        [TestMethod]
        public void GetAllMetaAuthorsPlusDocumentsEarlyActualize2()
        {
            using (var ctx = new TestDbContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var DocsByCreator = ctx.Meta
                    .Include(m => m.Document.MetaInfo) // Load all the metaInfo for each object
                    .Where(m => m.Name == "Author")
                    .ToList() // Actualize the Database
                    .Select(m => new { Doc = m.Document, Creator = m })
                    .ToList(); // Actualize the collection

                Assert.AreEqual(5, DocsByCreator.Count);

                foreach (var dc in DocsByCreator)
                {
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Author"), "All documents must have at least one Author");
                    Assert.IsTrue(dc.Doc.MetaInfo.Any(m => m.Name == "Created On"), "All documents must have a Created On meta value");
                }
            }
        }
    }
}
