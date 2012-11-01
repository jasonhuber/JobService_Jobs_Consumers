define('Mobile/Training/Views/Job/List', [
    'dojo/_base/declare',
    'dojo/_base/array',
    'dojo/string',
    'Mobile/SalesLogix/Action',
    'Sage/Platform/Mobile/List'
], function(
    declare,
    array,
    string,
    action,
    List
) {
    return declare('Mobile.Training.Views.Job.List', [List], {
        //Templates
        itemTemplate: new Simplate([
            '<h3>{%: $.jobId %}</h3>',
            '<h4>Phase:{%: $.phase %} Status:{%: $.status %} </h4>'
        ]),
		createRequest: function() {
		   var request = this.inherited(arguments);
//http://localhost:3333/sdata/$app/scheduling/%E2%80%90/executions?format=json
			request.setApplicationName('$app'); // changes slx to $app, sdata/$app/
			request.setContractName('scheduling'); // changes dynamic to mashup, so sdata/$app/mashup/-/
			request.setResourceKind('executions'); // so now we have sdata/$app/mashup/-/mashup
			request.getUri().setIncludeContent(true); // this changes the default false to true

		  return request;
		},

        //View Properties        
        icon: 'content/images/icons/Company_24.png',
        id: 'job_list',
        querySelect: [
            'jobId',
			'phase',
			'status'
        ],
        resourceKind: 'scheduling',
        allowSelection: false,
        enableActions: true,

    });
});