using System;
using System.Data;

using NHibernate.Util;

namespace NHibernate.Driver
{
	/// <summary>
	/// Base class for the implementation of IDriver
	/// </summary>
	public abstract class DriverBase : IDriver
	{
		public DriverBase()
		{
		}

		#region IDriver Members

		public abstract IDbConnection CreateConnection();

		public abstract IDbCommand CreateCommand();

		public abstract bool UseNamedPrefixInSql {get;}

		public abstract bool UseNamedPrefixInParameter {get;}

		public abstract string NamedPrefix {get;}

		public string FormatNameForSql(string parameterName)
		{
			return UseNamedPrefixInSql ? (NamedPrefix + parameterName): StringHelper.SqlParameter;
		}

		public string FormatNameForSql(string tableAlias, string parameterName)
		{
			if(!UseNamedPrefixInSql) return StringHelper.SqlParameter;

			
			if(tableAlias!=null && tableAlias!=String.Empty) 
			{
				return NamedPrefix + tableAlias + parameterName;
			}
			else 
			{
				return NamedPrefix + parameterName;
			}
		}

		public string FormatNameForParameter(string parameterName)
		{
			return UseNamedPrefixInParameter ? (NamedPrefix + parameterName) : parameterName;
		}

		public string FormatNameForParameter(string tableAlias, string parameterName)
		{
			if(!UseNamedPrefixInParameter) return parameterName;

			
			if(tableAlias!=null && tableAlias!=String.Empty) 
			{
				return NamedPrefix + tableAlias + parameterName;
			}
			else 
			{
				return NamedPrefix + parameterName;
			}
		}

		public bool SupportsMultipleOpenReaders 
		{
			get { return false;}
		}

		#endregion
	}
}
