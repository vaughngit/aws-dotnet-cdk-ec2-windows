# Welcome to your CDK C# project!

## CDK prerequisites: 
#### 1)  AWS CLI - Installing or updating the latest version of the AWS CLI: https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html
#### 2) Configure the AWS CLI:  https://docs.aws.amazon.com/cdk/v2/guide/cli.html#cli-environment
    aws configure

## Install the CDK: 
#### The AWS CDK Toolkit is installed via the Node Package Manager which can be found and downloaded from here (https://nodejs.org/en/download/) and for this project ensure the dotnet sdk is installed: (https://dotnet.microsoft.com/en-us/download)
    npm install -g aws-cdk             # install latest version



### Download this project via browser or git cli: (https://git-scm.com/downloads) 
    git clone https://github.com/vaughngit/awscdk-ec2-windows.git
    cd awscdk-ec2-windows
    dotnet build src
    cdk synth
    cdk bootstrap
    cdk deploy 
* note the "InstanceId", "Password", and "UserName" Outputs that will be displayed in your desktop terminal console once deployment completes. 

### RDP into instace from browser via the System Manager's Fleet Manager console feature: 
* The Google Chrome browser appears to provide the best experience (ie copy/paste features) 
   1)  From the AWS Home Page Console navigate to -> Systems Manager Console- > Fleet Manager feature under the "Node Management" header and the newly deployed ec2 instance should be listed in the "Managed nodes" list on this landing page.  
   2) Check the box next to the new windows ec2 instance
   3) Select the "Node action" dropdown in the top right-hand corner and select the "Connect with Remote Desktop" under the "Connect" header  
   4) Enter the username and password displayed in the your local desktop termainal console noted above and select the "Connect" button
   5) Once connected click on the instanceid in the header section to expand the remote display or select the full screen option in the top right-hand coner of the display.  

* See this reference for addditional guidance in setting up CDK within your AWS Environment: https://docs.aws.amazon.com/cdk/v2/guide/cli.html

## Useful commands
The `cdk.json` file tells the CDK Toolkit how to execute your app.

It uses the [.NET Core CLI](https://docs.microsoft.com/dotnet/articles/core/) to compile and execute your project.

* `dotnet build src` compile this app
* `cdk deploy`       deploy this stack to your default AWS account/region
* `cdk diff`         compare deployed stack with current state
* `cdk synth`        emits the synthesized CloudFormation template