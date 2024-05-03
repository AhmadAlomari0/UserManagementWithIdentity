using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Security;

#nullable disable

namespace UserManagementWithIdentity.Data.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES(N'83e44d5a-4f5c-4e39-8761-399838e89109', N'admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEATNT4W5PHvabw1gq289g2LHXm4XnP0bTYAFxheYMW0mAg6dLavMXrl7MK2NdlvKDA==', N'QZ73OHVUHDOHEC4MB262JWUA3RGWAIEB', N'0e05a223-c377-4628-a40d-d07ffe0bb055', NULL, 0, 0, NULL, 1, 0, N'Ahmad', N'Alomari',null)");

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id = '83e44d5a-4f5c-4e39-8761-399838e89109' ");
        }
    }
}
