using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrELP.Infrastructure.Migrations
{
    public partial class dbUpdatev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvanceLimit",
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
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 979, DateTimeKind.Local).AddTicks(5418));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "RequestTypes",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 232, DateTimeKind.Local).AddTicks(1797),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 979, DateTimeKind.Local).AddTicks(5195));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Companies",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(6860),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(8523));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Companies",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(6647),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(8041));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(2107),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(3612));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(1794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 215, DateTimeKind.Local).AddTicks(6175),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 963, DateTimeKind.Local).AddTicks(1060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 215, DateTimeKind.Local).AddTicks(5915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 963, DateTimeKind.Local).AddTicks(800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Addresses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(4394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(5796));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Addresses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(4164),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(5567));

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityNumber",
                table: "AspNetUsers",
                column: "IdentityNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdentityNumber",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "RequestTypes",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 979, DateTimeKind.Local).AddTicks(5418),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 232, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "RequestTypes",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 979, DateTimeKind.Local).AddTicks(5195),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 232, DateTimeKind.Local).AddTicks(1797));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Companies",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(8523),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(6860));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Companies",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(8041),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(6647));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(3612),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(2107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(3198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(1794));

            migrationBuilder.AddColumn<decimal>(
                name: "AdvanceLimit",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 963, DateTimeKind.Local).AddTicks(1060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 215, DateTimeKind.Local).AddTicks(6175));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 963, DateTimeKind.Local).AddTicks(800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 215, DateTimeKind.Local).AddTicks(5915));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Addresses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(5796),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(4394));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Addresses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 22, 42, 0, 978, DateTimeKind.Local).AddTicks(5567),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 23, 35, 19, 231, DateTimeKind.Local).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "feeeec80-3fdc-4522-b9b6-8ee0454ce07a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9f5c2be7-c928-4488-96ea-a8fa21c0d083");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "09746546-8ddf-45fb-8d41-73b430718666");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 10, 24, 22, 42, 0, 964, DateTimeKind.Local).AddTicks(6637), "c8e6e327-bf16-4103-bc86-349b0a03b307", new DateTime(2023, 10, 24, 22, 42, 0, 964, DateTimeKind.Local).AddTicks(6636), "AQAAAAEAACcQAAAAEKpg5uc9rx+16E5r/Xm/AgYQRZSp8oH8gcKyRkeEi7dG22zp49makAaSZcxJMvbL+g==", "4b5c6ae4-74f8-49c5-9217-890f34d08819" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2023, 10, 24, 22, 42, 0, 964, DateTimeKind.Local).AddTicks(6687), "28e5a172-463c-460a-b939-84cf0a1abc3b", new DateTime(2023, 10, 24, 22, 42, 0, 964, DateTimeKind.Local).AddTicks(6687), "AQAAAAEAACcQAAAAEAj/zTAInfhyMxNt6PY2xDgGyFR4p3rUbHtYLViVM8RVh+69OfQXycUGSTxyqAlE2A==", "6b625e78-1c56-44b5-8ada-7e19143c1f0c" });
        }
    }
}
