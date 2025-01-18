using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientTypes_IngredientTypeID",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientID",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeID",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeNationalities_RecipeNationalityID",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeTypes_RecipeTypeID",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "RecipeTypeID",
                table: "Recipes",
                newName: "RecipeTypeId");

            migrationBuilder.RenameColumn(
                name: "RecipeNationalityID",
                table: "Recipes",
                newName: "RecipeNationalityId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Recipes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeTypeID",
                table: "Recipes",
                newName: "IX_Recipes_RecipeTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeNationalityID",
                table: "Recipes",
                newName: "IX_Recipes_RecipeNationalityId");

            migrationBuilder.RenameColumn(
                name: "RecipeID",
                table: "RecipeIngredients",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "IngredientID",
                table: "RecipeIngredients",
                newName: "IngredientId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "RecipeIngredients",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_RecipeID",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_IngredientID",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_IngredientId");

            migrationBuilder.RenameColumn(
                name: "IngredientTypeID",
                table: "Ingredients",
                newName: "IngredientTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_IngredientTypeID",
                table: "Ingredients",
                newName: "IX_Ingredients_IngredientTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientTypes_IngredientTypeId",
                table: "Ingredients",
                column: "IngredientTypeId",
                principalTable: "IngredientTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeNationalities_RecipeNationalityId",
                table: "Recipes",
                column: "RecipeNationalityId",
                principalTable: "RecipeNationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeTypes_RecipeTypeId",
                table: "Recipes",
                column: "RecipeTypeId",
                principalTable: "RecipeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientTypes_IngredientTypeId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeNationalities_RecipeNationalityId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeTypes_RecipeTypeId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "RecipeTypeId",
                table: "Recipes",
                newName: "RecipeTypeID");

            migrationBuilder.RenameColumn(
                name: "RecipeNationalityId",
                table: "Recipes",
                newName: "RecipeNationalityID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Recipes",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeTypeId",
                table: "Recipes",
                newName: "IX_Recipes_RecipeTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeNationalityId",
                table: "Recipes",
                newName: "IX_Recipes_RecipeNationalityID");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeIngredients",
                newName: "RecipeID");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "RecipeIngredients",
                newName: "IngredientID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RecipeIngredients",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_RecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_IngredientID");

            migrationBuilder.RenameColumn(
                name: "IngredientTypeId",
                table: "Ingredients",
                newName: "IngredientTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_IngredientTypeId",
                table: "Ingredients",
                newName: "IX_Ingredients_IngredientTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientTypes_IngredientTypeID",
                table: "Ingredients",
                column: "IngredientTypeID",
                principalTable: "IngredientTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientID",
                table: "RecipeIngredients",
                column: "IngredientID",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeID",
                table: "RecipeIngredients",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeNationalities_RecipeNationalityID",
                table: "Recipes",
                column: "RecipeNationalityID",
                principalTable: "RecipeNationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeTypes_RecipeTypeID",
                table: "Recipes",
                column: "RecipeTypeID",
                principalTable: "RecipeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
