package ro.andrei15193.bitbucketbrowser;

import ro.andrei15193.bitbucketbrowser.model.IObserver;
import ro.andrei15193.bitbucketbrowser.model.IUsernameContainer;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public final class UsernameSelectionActivity extends ActionBarActivity
{
	@Override
	protected final void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_username_selection);

		final EditText usernameInput = (EditText)findViewById(R.id.usernameInput);
		final UsernameListAdapter usernameListAdapter = new UsernameListAdapter(_getApplication().getUsernameContainer());

		((ListView)findViewById(R.id.usersList)).setAdapter(usernameListAdapter);
		((Button)findViewById(R.id.browseUsernameAction)).setOnClickListener(new OnClickListener()
		{
			@Override
			public void onClick(View v)
			{
				String username = usernameInput.getText().toString();

				if (username.length() == 0)
					Toast.makeText(UsernameSelectionActivity.this, "Please type a username", Toast.LENGTH_SHORT).show();
				else
				{
					BitBucketBrowserApplication applicationContext = _getApplication();

					applicationContext.getUsernameContainer().add(username);
					applicationContext.getUserRepository().add(username);
					applicationContext.setUsername(username);
					startActivity(new Intent(UsernameSelectionActivity.this, RepositoriesActivity.class));
				}
			}
		});
	}

	private final BitBucketBrowserApplication _getApplication()
	{
		return (BitBucketBrowserApplication)getApplication();
	}

	private final class UsernameListAdapter extends BaseAdapter
	{
		public UsernameListAdapter(IUsernameContainer usernameContainer)
		{
			if (usernameContainer == null)
				throw new NullPointerException();
			_usernameContainer = usernameContainer;

			_usernameContainer.subscribe(new IObserver()
			{
				@Override
				public final void onSubjectChanged()
				{
					UsernameListAdapter.this.notifyDataSetChanged();
				}
			});
		}

		@Override
		public final int getCount()
		{
			return _usernameContainer.size();
		}
		@Override
		public final String getItem(int position)
		{
			return _usernameContainer.get(position).getUsername();
		}
		@Override
		public final long getItemId(int position)
		{
			return position;
		}
		@Override
		public final View getView(int position, View convertView, ViewGroup parent)
		{
			TextView userTextView = (TextView)convertView;
			final String username = getItem(position);

			if (userTextView == null)
				userTextView = (TextView)LayoutInflater.from(UsernameSelectionActivity.this).inflate(R.layout.partial_user, null);

			userTextView.setOnClickListener(new OnClickListener()
			{
				@Override
				public void onClick(View v)
				{
					EditText usernameEditText = (EditText)findViewById(R.id.usernameInput);
					usernameEditText.setText(username);
					usernameEditText.setSelection(username.length(), username.length());
				}
			});
			userTextView.setText(username);

			return userTextView;
		}

		private final IUsernameContainer _usernameContainer;
	}
}