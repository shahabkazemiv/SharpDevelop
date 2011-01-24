﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Linq;

using NuGet;

namespace ICSharpCode.PackageManagement
{
	public class AvailablePackagesViewModel : PackagesViewModel
	{
		public AvailablePackagesViewModel(IPackageManagementService packageManagementService)
			: base(packageManagementService)
		{
			IsSearchable = true;
			ShowPackageSources = packageManagementService.HasMultiplePackageSources;
		}
		
		protected override IQueryable<IPackage> GetAllPackages()
		{
			return PackageManagementService.ActivePackageRepository.GetPackages();
		}
		
		protected override IEnumerable<IPackage> GetFilteredPackagesBeforePagingResults(IQueryable<IPackage> allPackages)
		{
			IEnumerable<IPackage> filteredPackages = base.GetFilteredPackagesBeforePagingResults(allPackages);
			return GetDistinctPackagesById(filteredPackages);
		}
		
		IEnumerable<IPackage> GetDistinctPackagesById(IEnumerable<IPackage> allPackages)
		{
			return allPackages.DistinctLast<IPackage>(PackageEqualityComparer.Id);
		}
	}
}
