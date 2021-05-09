namespace Proffer.Templating.Handlerbars.Tests.Stubs
{
    using System.Collections.Generic;

    public class ContactsContext
    {
        public List<Contact> Contacts = new();

        public class Contact
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
