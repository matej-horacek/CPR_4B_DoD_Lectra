using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixEntitiesAndIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_processing_results_recordings_RecordingId",
                table: "processing_results");

            migrationBuilder.DropForeignKey(
                name: "FK_recordings_User_UserId",
                table: "recordings");

            migrationBuilder.DropForeignKey(
                name: "FK_recordings_subjects_SubjectId",
                table: "recordings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subjects",
                table: "subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recordings",
                table: "recordings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processing_results",
                table: "processing_results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "subjects",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "subjects",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_subjects_Name",
                table: "subjects",
                newName: "ix_subjects_name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "recordings",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "recordings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "recordings",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "recordings",
                newName: "subject_id");

            migrationBuilder.RenameColumn(
                name: "NotePath",
                table: "recordings",
                newName: "note_path");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "recordings",
                newName: "file_path");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "recordings",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_recordings_UserId",
                table: "recordings",
                newName: "ix_recordings_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_recordings_SubjectId",
                table: "recordings",
                newName: "ix_recordings_subject_id");

            migrationBuilder.RenameIndex(
                name: "IX_recordings_NotePath",
                table: "recordings",
                newName: "ix_recordings_note_path");

            migrationBuilder.RenameIndex(
                name: "IX_recordings_FilePath",
                table: "recordings",
                newName: "ix_recordings_file_path");

            migrationBuilder.RenameColumn(
                name: "Step",
                table: "processing_results",
                newName: "step");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "processing_results",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "processing_results",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RecordingId",
                table: "processing_results",
                newName: "recording_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "processing_results",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_processing_results_RecordingId",
                table: "processing_results",
                newName: "ix_processing_results_recording_id");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AspNetUsers",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "AspNetUsers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetUsers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AspNetUsers",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                newName: "two_factor_enabled");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "AspNetUsers",
                newName: "security_stamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                newName: "phone_number_confirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "AspNetUsers",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "AspNetUsers",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                newName: "normalized_user_name");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                newName: "normalized_email");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "AspNetUsers",
                newName: "lockout_end");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                newName: "lockout_enabled");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                newName: "email_confirmed");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AspNetUsers",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "AspNetUsers",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "AspNetUsers",
                newName: "access_failed_count");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "recordings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "note_path",
                table: "recordings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "recordings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "step",
                table: "processing_results",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "processing_results",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "normalized_user_name",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "normalized_email",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "pk_subjects",
                table: "subjects",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_recordings",
                table: "recordings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_processing_results",
                table: "processing_results",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_users",
                table: "AspNetUsers",
                column: "id");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_processing_results_recordings_recording_id",
                table: "processing_results",
                column: "recording_id",
                principalTable: "recordings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recordings_subjects_subject_id",
                table: "recordings",
                column: "subject_id",
                principalTable: "subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_recordings_users_user_id",
                table: "recordings",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_processing_results_recordings_recording_id",
                table: "processing_results");

            migrationBuilder.DropForeignKey(
                name: "fk_recordings_subjects_subject_id",
                table: "recordings");

            migrationBuilder.DropForeignKey(
                name: "fk_recordings_users_user_id",
                table: "recordings");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_subjects",
                table: "subjects");

            migrationBuilder.DropPrimaryKey(
                name: "pk_recordings",
                table: "recordings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_processing_results",
                table: "processing_results");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_users",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "subjects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "subjects",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_subjects_name",
                table: "subjects",
                newName: "IX_subjects_Name");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "recordings",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "recordings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "recordings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "subject_id",
                table: "recordings",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "note_path",
                table: "recordings",
                newName: "NotePath");

            migrationBuilder.RenameColumn(
                name: "file_path",
                table: "recordings",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "recordings",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_recordings_user_id",
                table: "recordings",
                newName: "IX_recordings_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_recordings_subject_id",
                table: "recordings",
                newName: "IX_recordings_SubjectId");

            migrationBuilder.RenameIndex(
                name: "ix_recordings_note_path",
                table: "recordings",
                newName: "IX_recordings_NotePath");

            migrationBuilder.RenameIndex(
                name: "ix_recordings_file_path",
                table: "recordings",
                newName: "IX_recordings_FilePath");

            migrationBuilder.RenameColumn(
                name: "step",
                table: "processing_results",
                newName: "Step");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "processing_results",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "processing_results",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "recording_id",
                table: "processing_results",
                newName: "RecordingId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "processing_results",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_processing_results_recording_id",
                table: "processing_results",
                newName: "IX_processing_results_RecordingId");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "User",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "User",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "two_factor_enabled",
                table: "User",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "security_stamp",
                table: "User",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phone_number_confirmed",
                table: "User",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "User",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "User",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalized_user_name",
                table: "User",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalized_email",
                table: "User",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockout_end",
                table: "User",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockout_enabled",
                table: "User",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "email_confirmed",
                table: "User",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "User",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                table: "User",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "access_failed_count",
                table: "User",
                newName: "AccessFailedCount");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "recordings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NotePath",
                table: "recordings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "recordings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<int>(
                name: "Step",
                table: "processing_results",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "processing_results",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "User",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subjects",
                table: "subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recordings",
                table: "recordings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processing_results",
                table: "processing_results",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_processing_results_recordings_RecordingId",
                table: "processing_results",
                column: "RecordingId",
                principalTable: "recordings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recordings_User_UserId",
                table: "recordings",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_recordings_subjects_SubjectId",
                table: "recordings",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
