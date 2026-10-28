using Microsoft.EntityFrameworkCore;
using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Data
{
    public static class DevelopmentDataSeeder
    {
        private const string LibraryName = "Biblioteca Central de Desarrollo";
        private const string LibraryAddress = "Av. Desarrollo 100";
        private const string BranchName = "Sucursal Centro";
        private const string BranchAddress = "Av. Desarrollo 100";

        public static async Task<(int LibraryId, int LibraryBranchId)> SeedAsync(
            TheLibraryDbContext context)
        {
            var library = await context.Libraries
                .FirstOrDefaultAsync(library =>
                    library.Name == LibraryName &&
                    library.Address == LibraryAddress);

            if (library is null)
            {
                library = new Library
                {
                    Name = LibraryName,
                    Address = LibraryAddress
                };

                context.Libraries.Add(library);
                await context.SaveChangesAsync();
            }

            var branch = await context.LibraryBranches
                .FirstOrDefaultAsync(branch =>
                    branch.LibraryId == library.Id &&
                    branch.Name == BranchName &&
                    branch.Address == BranchAddress);

            if (branch is null)
            {
                branch = new LibraryBranch
                {
                    Name = BranchName,
                    Address = BranchAddress,
                    LibraryId = library.Id
                };

                context.LibraryBranches.Add(branch);
                await context.SaveChangesAsync();
            }

            return (library.Id, branch.Id);
        }
    }
}
