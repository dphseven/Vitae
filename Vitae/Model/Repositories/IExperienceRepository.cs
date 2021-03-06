﻿namespace Vitae.Model
{
    using System;
    using System.Collections.Generic;
    using Vitae.Services;

    public interface IExperienceRepository : IRepository<IExperienceEntity>
    {
        IList<IDecoratedExperienceEntity> GetAllDecorators();
        IList<IExperienceItem> GetAllExperienceItems();
        IList<IJobTitle> GetAllJobTitles();
        IList<IJobTitle> GetJobTitlesForJob(Guid jobID);
    }
}