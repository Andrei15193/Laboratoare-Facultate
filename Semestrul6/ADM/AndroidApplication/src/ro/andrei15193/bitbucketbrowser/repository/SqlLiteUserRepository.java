package ro.andrei15193.bitbucketbrowser.repository;

import java.util.ArrayList;
import java.util.List;

import ro.andrei15193.bitbucketbrowser.model.Username;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class SqlLiteUserRepository extends SQLiteOpenHelper implements IUserRepository
{
	public SqlLiteUserRepository(Context context)
	{
		super(context, SqlLiteUserRepository.DATABASE_NAME, null, SqlLiteUserRepository.DATABASE_VERSION);
	}

	@Override
	public void add(String username)
	{
		Cursor cursor = null;
		try
		{
			cursor = getReadableDatabase().rawQuery("select accessCount from BitBucketUsers where username = ?;", new String[] {username});

			if (cursor.moveToFirst())
				getWritableDatabase().execSQL("update BitBucketUsers set accessCount = " + (cursor.getInt(0) + 1) + " where username = '" + username + "'");
			else
				getWritableDatabase().execSQL("insert into BitBucketUsers (username, accessCount) values ('" + username + "', 1)");
		}
		finally
		{
			if (cursor != null)
				cursor.close();
		}
	}
	@Override
	public Iterable<Username> allUsers()
	{
		List<Username> usernames = new ArrayList<Username>();

		Cursor cursor = null;
		try
		{
			cursor = getReadableDatabase().rawQuery("select username, accessCount from BitBucketUsers;", new String[0]);

			while (cursor.moveToNext())
				usernames.add(new Username(cursor.getString(0), cursor.getInt(1)));
		}
		finally
		{
			if (cursor != null)
				cursor.close();
		}
		return usernames;
	}
	@Override
	public void onCreate(SQLiteDatabase database)
	{
		database.execSQL("create table BitBucketUsers (username text primary key, accessCount integer);");
	}
	@Override
	public void onUpgrade(SQLiteDatabase database, int oldVersion, int newVersion)
	{
		database.execSQL("drop table if exists BitBucketUsers");
		onCreate(database);
	}

	private static final String DATABASE_NAME = "BitBucketBrowser.db";
	private static final int DATABASE_VERSION = 1;
}