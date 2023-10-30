using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrELP.Infrastructure.Migrations
{
    public partial class requestTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "RequestTypes",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(8317),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 232, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "RequestTypes",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(8103),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 232, DateTimeKind.Local).AddTicks(1797));

            migrationBuilder.AddColumn<int>(
                name: "TotalDaysOff",
                table: "LeaveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Companies",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(3486),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(6860));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Companies",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(3272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(6647));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 452, DateTimeKind.Local).AddTicks(6144),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(2107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 452, DateTimeKind.Local).AddTicks(5803),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(1794));

            migrationBuilder.AddColumn<int>(
                name: "DayOffsLeft",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxDaysOff",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 438, DateTimeKind.Local).AddTicks(2045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 215, DateTimeKind.Local).AddTicks(6175));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 438, DateTimeKind.Local).AddTicks(1735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 215, DateTimeKind.Local).AddTicks(5915));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Addresses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(621),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(4394));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Addresses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 456, DateTimeKind.Local).AddTicks(9945),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "73e78ed6-a33b-4f49-a473-dfe12276b38a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e0a96523-fb12-4321-b976-8d03d332006b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7feff36b-0823-4fb2-bc02-6518a9064327");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 10, 30, 18, 50, 45, 439, DateTimeKind.Local).AddTicks(2878), "4436a82e-e6d9-4fb7-bcdd-cb83a3c31c0d", new DateTime(2023, 10, 30, 18, 50, 45, 439, DateTimeKind.Local).AddTicks(2877), "AQAAAAEAACcQAAAAEERQ+OSnldaKKruwuBDh9FNspYYXOQTPFR3H2C1Fmar11OvN2dyDF8GBHAP0jbWnow==", "d6be313a-5976-440c-b61a-ce515297c776" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 10, 30, 18, 50, 45, 439, DateTimeKind.Local).AddTicks(2901), "ebc11e72-f7c2-4d8c-a5f1-a6eeb85e4c3e", new DateTime(2023, 10, 30, 18, 50, 45, 439, DateTimeKind.Local).AddTicks(2900), "AQAAAAEAACcQAAAAEKWh/VFG9SxdRVrwDd3aBKGLwRMkyHvQTHGOUe3FK60kzEjRvy6TCY7Q3Ht6cO4vUA==", "178b14e0-89d0-4f4d-8112-b66888716284" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDaysOff",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "DayOffsLeft",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaxDaysOff",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "RequestTypes",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 232, DateTimeKind.Local).AddTicks(2005),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(8317));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "RequestTypes",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 232, DateTimeKind.Local).AddTicks(1797),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(8103));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Companies",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(6860),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(3486));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Companies",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(6647),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(3272));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(2107),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 452, DateTimeKind.Local).AddTicks(6144));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(1794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 452, DateTimeKind.Local).AddTicks(5803));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 215, DateTimeKind.Local).AddTicks(6175),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 438, DateTimeKind.Local).AddTicks(2045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 215, DateTimeKind.Local).AddTicks(5915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 438, DateTimeKind.Local).AddTicks(1735));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Addresses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(4394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 457, DateTimeKind.Local).AddTicks(621));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Addresses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(4164),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 30, 18, 50, 45, 456, DateTimeKind.Local).AddTicks(9945));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4112f46f-b3fd-4cfc-a4bd-c3109f78cd20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "becaef41-50d4-4bfb-b368-11b83b4e9d11");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3675f8cf-46d4-417c-8792-9242db80dc1c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 10, 24, 23, 35, 19, 216, DateTimeKind.Local).AddTicks(7436), "d12afa4a-8ac9-4f4d-be50-2430e77308cc", new DateTime(2023, 10, 24, 23, 35, 19, 216, DateTimeKind.Local).AddTicks(7436), "AQAAAAEAACcQAAAAEFiP3S8GRGh0BWkIucOUmVbF89Z5PMIXwPxqNelTHv5n93gwJAhmfik9Udv2V5+xZQ==", "037e0b01-da37-4a6d-97a3-3ac0a3875520" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 10, 24, 23, 35, 19, 216, DateTimeKind.Local).AddTicks(7458), "f604d460-427f-499e-a5fe-a51543e0577b", new DateTime(2023, 10, 24, 23, 35, 19, 216, DateTimeKind.Local).AddTicks(7457), "AQAAAAEAACcQAAAAEOrU62xBG3CA1Sgc1rBHtbsVrSPwXapcNa4nTusCz+fOTug2tIt5y/sXjQHdKmsbiA==", "5aebdf5f-e591-4fc1-bf5b-5cd97ccacec2" });
        }
    }
}
