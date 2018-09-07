using Marten.Testing.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Marten.Testing
{
    public class SchemaObjectCreation
    {
        [Fact]
        public async Task should_not_recreate_existing_matching_table()
        {
            using (var store = DocumentStore.For("Server=localhost; Database=postgres; User Id=postgres; Password=postgres;"))
            {
                using (var session = store.OpenSession())
                {
                    var users = await session.Query<User>().ToListAsync();
                }
            }

            // this second connection string just needs to use a login that isn't a superuser and isn't the owner of the previously created mt_doc_user table
            using (var store = DocumentStore.For("Server = localhost; Database = postgres; Integrated Security = true;"))
            {
                using (var session = store.OpenSession())
                {
                    session.Store(new User());
                    session.SaveChanges();
                }
            }
        }
    }
}
