﻿using Git.DataForm;
using Git.Models;
using System.Collections.Generic;

namespace Git.Contracts
{
    public interface ICommitService
    {
        (bool, List<ErrorViewModel>) ValidateCommit(CreateCommitDataForm commitDataForm);

        List<CommitViewModel> GetAllCommits();

        void CreateCommit(CreateCommitDataForm commitDataForm);
    }
}