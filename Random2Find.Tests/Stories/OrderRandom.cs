using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Find.ClientConventions;
using FluentAssertions;
using StoryQ;
using Xunit;
using System.Threading;
using EPiServer.Find;

namespace Random2Find.Tests.Stories
{
    public class OrderRandom
    {
        [Fact]
        public void FilterByKeyValueInDictionary()
        {
            new Story("Randomly order hits in a search result")
                .InOrderTo("be able to randomly order hits")
                .AsA("developer")
                .IWant("to be able to sort a search result randomly")
                .WithScenario("randomly sort search result")
                .Given(IHaveAClient)
                    .And(IHaveASetOfDocuments)
                    .And(IHaveIndexedTheDocuments)
                    .And(IHaveWaitedForASecond)
                .When(ISearchDocumentsWithRandomOrderTwice)
                .Then(IShouldGetRandomlyOrderedResults)
                .Execute();
        }

        protected IClient client;
        void IHaveAClient()
        {
            client = Client.CreateFromConfig();
        }

        private List<Document> documents;
        void IHaveASetOfDocuments()
        {
            documents = new List<Document>();

            for (int i = 0; i < 10; i++)
            {
                var document = new Document() { Name = "doc" + i };
                documents.Add(document);
            }
        }

        void IHaveIndexedTheDocuments()
        {
            client.Index(documents);
        }

        void IHaveWaitedForASecond()
        {
            Thread.Sleep(1000);
        }

        SearchResults<Document> result1;
        SearchResults<Document> result2;
        void ISearchDocumentsWithRandomOrderTwice()
        {
            result1 = client.Search<Document>()
                        .OrderRandom()
                        .GetResult();

            result2 = client.Search<Document>()
                        .OrderRandom()
                        .GetResult();
        }

        void IShouldGetRandomlyOrderedResults()
        {
            result1.Select(x => x.Name).Should().NotEqual(result2.Select(x => x.Name));
        }

        public class Document
        {
            public string Name { get; set; }
        }
    }
}
