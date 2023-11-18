using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eTickets.Data.Services;

public interface ICinemasService : IEntityBaseRepository<Cinema>
{
    
}