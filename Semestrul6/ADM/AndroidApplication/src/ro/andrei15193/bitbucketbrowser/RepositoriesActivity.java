package ro.andrei15193.bitbucketbrowser;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONObject;

import ro.andrei15193.bitbucketbrowser.model.Repository;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

public class RepositoriesActivity extends ActionBarActivity
{
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		_username = _getApplication().getUsername();

		setContentView(R.layout.activity_repositories);
		setTitle(_username + "'s Repositories");
	}
	@Override
	protected final void onResume()
	{
		super.onResume();
		if (_loadTask == null)
			_loadTask = new GetRepositoriesTask().execute();
	}
	@Override
	protected final void onPause()
	{
		super.onPause();
		if (_loadTask != null)
		{
			_loadTask.cancel(true);
			_loadTask = null;
		}
	}

	private final BitBucketBrowserApplication _getApplication()
	{
		return (BitBucketBrowserApplication)getApplication();
	}

	private String _username;
	private AsyncTask<Void, Void, List<Repository>> _loadTask = null;

	private class GetRepositoriesTask extends AsyncTask<Void, Void, List<Repository>>
	{
		@Override
		protected List<Repository> doInBackground(Void... params)
		{
			String username = _username;
			List<Repository> repositories = new ArrayList<Repository>();
			DefaultHttpClient httpClient = null;

			try
			{
				httpClient = new DefaultHttpClient();
				HttpResponse response = httpClient.execute(new HttpGet("https://bitbucket.org/api/2.0/repositories/" + username));
				JSONArray jsonRepositories = new JSONObject(_readText(response.getEntity().getContent())).getJSONArray("values");

				for (int jsonRepositoryIndex = 0; jsonRepositoryIndex < jsonRepositories.length(); jsonRepositoryIndex++)
				{
					JSONObject jsonRepository = jsonRepositories.getJSONObject(jsonRepositoryIndex);

					repositories.add(new Repository(jsonRepository.getString("name"), jsonRepository.getString("language"), jsonRepository.getJSONObject("owner").getString("username"), jsonRepository.getString("description")));
				}
			}
			catch (Exception exception)
			{
				_exception = exception;
			}
			finally
			{
				if (httpClient != null)
					httpClient.getConnectionManager().shutdown();
			}

			return repositories;
		}
		@Override
		protected void onPostExecute(List<Repository> repositories)
		{
			((ProgressBar)findViewById(R.id.repositoriesProgressBar)).setVisibility(View.GONE);

			if (_exception != null)
			{
				if (_exception.getClass() != InterruptedException.class)
					Toast.makeText(RepositoriesActivity.this, "Sorry, could not load repositories...", Toast.LENGTH_LONG).show();
				_loadTask = null;
			}

			((ListView)findViewById(R.id.repositoriesList)).setAdapter(new RepositoriesListAdapter(repositories));
		}

		private String _readText(InputStream content) throws IOException
		{
			final StringBuilder stringBuilder = new StringBuilder();
			final BufferedReader reader = new BufferedReader(new InputStreamReader(content));
			String line = reader.readLine();

			while (line != null)
			{
				stringBuilder.append(line);
				line = reader.readLine();
			}

			return stringBuilder.toString();
		}

		private Exception _exception = null;

		private class RepositoriesListAdapter extends BaseAdapter
		{
			public RepositoriesListAdapter(List<Repository> repositories)
			{
				_repositories = repositories;
			}

			@Override
			public int getCount()
			{
				return _repositories.size();
			}
			@Override
			public Repository getItem(int position)
			{
				return _repositories.get(position);
			}
			@Override
			public long getItemId(int position)
			{
				return position;
			}
			@Override
			public View getView(int position, View convertView, ViewGroup parent)
			{
				TextView repositoryTextView = (TextView)convertView;
				final Repository repository = getItem(position);

				if (repositoryTextView == null)
					repositoryTextView = (TextView)LayoutInflater.from(RepositoriesActivity.this).inflate(R.layout.partial_repository, null);

				repositoryTextView.setOnClickListener(new OnClickListener()
				{
					@Override
					public void onClick(View v)
					{
						_getApplication().setRepository(repository);
						startActivity(new Intent(RepositoriesActivity.this, RepositoryActivity.class));
					}
				});
				repositoryTextView.setText(repository.getName());

				return repositoryTextView;
			}

			private final List<Repository> _repositories;
		}
	}
}