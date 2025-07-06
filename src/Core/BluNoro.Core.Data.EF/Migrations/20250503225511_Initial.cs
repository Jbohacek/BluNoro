using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BluNoro.Core.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ChatPicPath = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    ChatOptions = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: false),
                    CreationOfCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastTimeEdited = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbSavedFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSavedFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    HashPassword = table.Column<string>(type: "TEXT", maxLength: 61, nullable: false),
                    ProfilePicPath = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserOptions = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: false),
                    LastLogIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    ChatsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.ChatsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ChatUser_tbChats_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "tbChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_tbUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UnformatedMessage = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    SenderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivedTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbChatMessages_tbChats_ParentChatId",
                        column: x => x.ParentChatId,
                        principalTable: "tbChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbChatMessages_tbUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    FileType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MessageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SavedFileId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbAttachments_tbChatMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "tbChatMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbAttachments_tbSavedFile_SavedFileId",
                        column: x => x.SavedFileId,
                        principalTable: "tbSavedFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_UsersId",
                table: "ChatUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_tbAttachments_MessageId",
                table: "tbAttachments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_tbAttachments_SavedFileId",
                table: "tbAttachments",
                column: "SavedFileId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChatMessages_ParentChatId",
                table: "tbChatMessages",
                column: "ParentChatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChatMessages_SenderId",
                table: "tbChatMessages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropTable(
                name: "tbAttachments");

            migrationBuilder.DropTable(
                name: "tbChatMessages");

            migrationBuilder.DropTable(
                name: "tbSavedFile");

            migrationBuilder.DropTable(
                name: "tbChats");

            migrationBuilder.DropTable(
                name: "tbUsers");
        }
    }
}
