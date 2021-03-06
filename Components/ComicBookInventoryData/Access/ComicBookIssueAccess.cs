﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookInventory.Data.Models;
using Dapper;
using System.Data.SqlClient;

namespace ComicBookInventory.Data.Access
{
    public class ComicBookIssueAccess : BaseAccess, Abstract.IIssueAccess
    {
        public ComicBookIssueAccess(Abstract.IAccess access) : base(access) { }

        public List<ComicBookIssue> GetComicBookIssuesForUser(Guid userId)
        {
            using (SqlConnection connection = GetOpenConnection())
            {
                return connection.Query<ComicBookIssue>("spComicBookIssueGetComicBookIssue", new { UserId = userId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public ComicBookIssue GetComicBookIssue(int issueId, Guid userId)
        {
            using (SqlConnection connection = GetOpenConnection())
            {
                return connection.Query<ComicBookIssue>("spComicBookIssueGetComicBookIssue", new { IssueId = issueId, UserId = userId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public bool SaveComicBookIssue(ComicBookIssue issue)
        {
            using (SqlConnection connection = GetOpenConnection())
            {
                int rowsChanged = connection.Execute("spComicBookIssueSaveComicBookIssue", issue, commandType: CommandType.StoredProcedure);

                return rowsChanged > 0;
            }
        }

        public bool RemoveComicBookIssue(int comicBookIssueId, Guid userId)
        {
            using (SqlConnection connection = GetOpenConnection())
            {
                int rowsChanged = connection.Execute("spComicBookIssueDeleteComicBookIssue", new { ComicBookIssueId = comicBookIssueId, UserId = userId }, commandType: CommandType.StoredProcedure);

                return rowsChanged > 0;
            }   
        }

    }
}
