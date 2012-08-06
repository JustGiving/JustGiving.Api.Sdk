using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [CollectionDataContract(Namespace = "")]
    public class PagesCreated : IEnumerable<PageCreated>
    {
        public PagesCreated(IEnumerable<PageCreated> innerCollection)
        {
            Pages = innerCollection.ToList();
        }

        public PagesCreated()
            : this(new List<PageCreated>())
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<PageCreated> GetEnumerator()
        {
            return Pages.GetEnumerator();
        }

        public void Add(PageCreated pageCreated)
        {
            Pages.Add(pageCreated);
        }

        [DataMember]
        public List<PageCreated> Pages { get; set; }
    }
}
