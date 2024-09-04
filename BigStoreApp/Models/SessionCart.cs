using BigStoreApp.Infrastructure.Extensions;
using Entities.Models;
using System.Text.Json.Serialization;

namespace BigStoreApp.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession? _session { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;

            SessionCart _cart = session?.GetJson<SessionCart>("cart") ?? new SessionCart();
            _cart._session = session;
            return _cart;
        }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            _session?.SetJson<SessionCart>("cart", this);
        }
        public override void ClearCart()
        {
            base.ClearCart();
            _session?.Remove("cart");
        }
        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            _session?.SetJson<SessionCart>("cart", this);
        }
        public override void IncreaseQuantity(Product product)
        {
            base.IncreaseQuantity(product);
            _session?.SetJson<SessionCart>("cart", this);
        }
        public override void DecreaseQuantity(Product product)
        {
            base.DecreaseQuantity(product);
            _session?.SetJson<SessionCart>("cart", this);
        }

    }
}
