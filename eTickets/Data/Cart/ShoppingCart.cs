using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie)
                .ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId};
        }

        public async Task AddItemToCartAsync(Movie movie)
        {
            var shoppingCartItem =
                await _context.ShoppingCartItems.FirstOrDefaultAsync(n =>
                    n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem is null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = this.ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCartAsync(Movie movie)
        {
            var shoppingCartItem =
                await _context.ShoppingCartItems.FirstOrDefaultAsync(n =>
                    n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            await _context.SaveChangesAsync();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }

        public double GetShoppingCartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
            .Select(n => n.Movie.Price * n.Amount).Sum();
    }
}
