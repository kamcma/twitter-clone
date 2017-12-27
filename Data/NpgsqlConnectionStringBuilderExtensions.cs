using System;

namespace Npgsql
{
    public static class NpgsqlConnectionStringBuilderExtensions
    {
        public static NpgsqlConnectionStringBuilder FromUri(this NpgsqlConnectionStringBuilder builder, Uri uri)
        {
            builder.Username = uri.UserInfo.Split(':')[0];
            builder.Password = uri.UserInfo.Split(':').Length > 1 ? uri.UserInfo.Split(':')[1] : String.Empty;
            builder.Host = uri.Host;
            builder.Database = uri.AbsolutePath.Trim('/');
            builder.Port = uri.Port;

            return builder;
        }
    }
}