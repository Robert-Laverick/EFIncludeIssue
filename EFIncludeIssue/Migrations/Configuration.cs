namespace EFIncludeIssue.Migrations
{
    using EFIncludeIssue.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var id = Guid.Parse("34bfdedb-ccdc-41ee-9c83-45b0544213ac");

            var Doc1 = new Document { Id = id, Body = "Document Body Text 1", Name = "Document1" };
            context.Documents.AddOrUpdate(Doc1);

            id = Guid.Parse("1a531f26-bc29-4c7f-8bf3-9d5a1e9588ac");

            var meta1 = new MetaInfo { Id = id, DocumentId = Doc1.Id, Name = "Author", Value = "Jim" };
            context.Meta.AddOrUpdate(meta1);

            id = Guid.Parse("f8fe5257-cb52-4f03-86a8-3af436e93f0a");

            var meta2 = new MetaInfo { Id = id, DocumentId = Doc1.Id, Name = "Author", Value = "Dave" };
            context.Meta.AddOrUpdate(meta2);

            id = Guid.Parse("a6593b47-56b3-40b9-8e6f-a4bee9430cf2");

            var meta3 = new MetaInfo { Id = id, DocumentId = Doc1.Id, Name = "Created On", Value = "Monday" };
            context.Meta.AddOrUpdate(meta3);

            id = Guid.Parse("e5fd4b79-f18a-4251-9d2b-0a2161ec8779");

            var Doc2 = new Document { Id = id, Body = "Document Body Text 2", Name = "Document2" };
            context.Documents.AddOrUpdate(Doc2);

            id = Guid.Parse("c3128ff7-5bbd-47c6-96df-1c213fc1a3ea");

            var meta4 = new MetaInfo { Id = id, DocumentId = Doc2.Id, Name = "Author", Value = "Jim" };
            context.Meta.AddOrUpdate(meta4);

            id = Guid.Parse("7b7b59ec-4ca9-4025-b377-be3b72d182aa");

            var meta5 = new MetaInfo { Id = id, DocumentId = Doc2.Id, Name = "Created On", Value = "Tuesday" };
            context.Meta.AddOrUpdate(meta5);

            id = Guid.Parse("e317d48c-ce63-4586-9a4c-d804f20390a7");

            var Doc3 = new Document { Id = id, Body = "Document Body Text 3", Name = "Document3" };
            context.Documents.AddOrUpdate(Doc3);

            id = Guid.Parse("7eaf797d-3b7a-4c29-8d1d-52e7980fef28");

            var meta6 = new MetaInfo { Id = id, DocumentId = Doc3.Id, Name = "Author", Value = "Jim" };
            context.Meta.AddOrUpdate(meta6);

            id = Guid.Parse("1c3dc571-c3b7-4408-957e-bf6af5a0df48");

            var meta7 = new MetaInfo { Id = id, DocumentId = Doc3.Id, Name = "Author", Value = "Dave" };
            context.Meta.AddOrUpdate(meta7);

            id = Guid.Parse("00551eb9-796b-40ff-a3c4-5153a504567b");

            var meta8 = new MetaInfo { Id = id, DocumentId = Doc3.Id, Name = "Created On", Value = "Wednesday" };
            context.Meta.AddOrUpdate(meta8);
        }
    }
}
