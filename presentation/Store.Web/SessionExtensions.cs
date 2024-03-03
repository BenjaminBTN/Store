using Store.Web.Models;
using System.Text;

namespace Store.Web
{
    public static class SessionExtensions
    {
        private const string key = "Cart";

        public static void Set(this ISession session, CartViewModel value)
        {
            if(value == null)
                return;

            using(var stream = new MemoryStream())
            using(var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(value.Items.Count);

                foreach(var item in value.Items)
                {
                    writer.Write(item.Key);
                    writer.Write(item.Value);
                }

                writer.Write(value.Amount);

                session.Set(key, stream.ToArray());
            }
        }

        public static bool TryGetCart(this ISession session, out CartViewModel value)
        {
            if(session.TryGetValue(key, out byte[] buffer))
            {
                using(var stream = new MemoryStream(buffer))
                using(var writer = new BinaryReader(stream,Encoding.UTF8, true))
                {
                    value = new CartViewModel();

                    var length = writer.ReadInt32();

                    for(int i = 0; i < length; i++)
                    {
                        var id = writer.ReadInt32();
                        var count = writer.ReadInt32();

                        value.Items.Add(id, count);
                    }

                    value.Amount = writer.ReadDecimal();

                    return true;
                }
            }

            value = null;
            return false;
        }
    }
}
