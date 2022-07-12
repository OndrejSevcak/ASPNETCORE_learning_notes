ExecuteAsync()  -> executed a command one or multiple times and returns number of afected rows
                -> for inserts, updates or stored procedures, that does not return a value!

QueryAsync()    -> executes a query and maps results
                -> for querying data
                
QuerySingleAsync() -> throws an exception if there is not exactly on element in the sequence        
                
                
Example SQL procedure that returns a string:

create procedure dbo.proc_DA_registerUser(
	@UserName varchar(50),
	@PasswordHash varbinary(max),  --vytvoří api na základě pwd zadaného uživatelem
	@PasswordSalt varbinary(max)   --vytvoří api, jedná se o hmacsha512 key
)
as
begin
	begin try
		insert into dbo.DA_Users(UserName, PasswordHash, PasswordSalt)
		values(@UserName, @PasswordHash, @PasswordSalt)

		select 'ok'
	end try
	begin catch
		select ERROR_MESSAGE()
	end catch
end

C# code:

public async Task<string> RegisterUser(string username, byte[] pwdHash, byte[] pwdSalt)
{
    DynamicParameters par = new DynamicParameters();
    par.Add("@UserName", username);
    par.Add("@PasswordHash", pwdHash);
    par.Add("@PasswordSalt", pwdSalt);
    //par.Add("Result", dbType: DbType.String, direction: ParameterDirection.ReturnValue);


    using (SqlConnection con = new SqlConnection(_config.GetConnectionString("Local")))
    {
        try
        {
            con.Open();
            var result = await con.QuerySingleAsync<string>("Applications.dbo.proc_DA_registerUser", param: par, commandType: System.Data.CommandType.StoredProcedure);

            return result;

        }
        catch (Exception ex)
        {
            throw new Exception("Dapper exception: " + ex.Message);
        }
        finally
        {
            con.Dispose();
        }
    }
        }
