define('Mobile/Training/ApplicationModule', [
    'dojo/_base/declare',
    'dojo/_base/lang',
    'dojo/string',
    'dojo/query',
    'dojo/dom-class',
    'Mobile/SalesLogix/Format',
    'Sage/Platform/Mobile/ApplicationModule',
	'Mobile/Training/Views/Job/List',
], function(
    declare,
    lang,
    string,
    query,
    domClass,
    format,
    ApplicationModule,
	JobList
) {
   return declare('Mobile.Training.ApplicationModule', ApplicationModule, {
		    loadViews: function() {
            this.inherited(arguments);
			this.registerView(new JobList());
			},
		
		helloText: 'hello',
		
			onAccountMashupRequestSuccess: function(row, node, entry, response) {
				var notifications = response.$resources[0].contactCount;
				
				query('span', node).text(notifications);
				domClass.remove(node, 'content-loading');
				},
			onAccountMashupRequestFailure: function(row, node, entry, response) {
				console.warn(response);
			},
			getCust: function(row, node, value, entry) {
				
				//Thanks Tony (one of the developers for mobile helped with this - a lot)!
				var request = new Sage.SData.Client.SDataNamedQueryRequest(App.getService()); // special request that does the $queries part
				request.setApplicationName('$app'); // changes slx to $app, sdata/$app/
				request.setContractName('mashups'); // changes dynamic to mashup, so sdata/$app/mashup/-/
				request.setResourceKind('mashups'); // so now we have sdata/$app/mashup/-/mashup
				request.getUri().setCollectionPredicate("'AccountContactsCount'"); // sdata/$app/mashup/-/mashup('RemindersAndAlarms')
				request.setQueryName('execute'); // since this is a named query request this makes it into: sdata/$app/mashup/-/mashup('RemindersAndAlarms')/$queries/execute?
				request.getUri().setIncludeContent(true); // this changes the default false to true

				// the rest of the args
				request.setQueryArg('_resultName', 'getCount');
				//request.setQueryArg('UserId', App.context && App.context.user['$key']);
				
				request.setQueryArg('_EntityId', entry.$key);
				request.read({
					success: lang.hitch(this, this.onAccountMashupRequestSuccess, row, node, entry),
					failure: this.onAccountMashupRequestFailure,
					scope: this
					});
				},
        loadCustomizations: function() {
			this.inherited(arguments);
			
			this.registerCustomization('detail', 'account_detail', {
				at: function(row) { return row.name == 'WebAddress'; },
				type: 'insert',
				where: 'before',
				value: {
					name: 'NumContacts',
					value: 'Loading...',
					cls: 'content-loading',
					label: '# Contacts',
					onCreate: this.getCust.bindDelegate(this)
				}
			});
			}

	})
	});