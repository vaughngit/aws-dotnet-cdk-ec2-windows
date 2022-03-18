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

### See this reference for addditional guidance in setting up CDK within your AWS Environment: https://docs.aws.amazon.com/cdk/v2/guide/cli.html


## Useful commands
The `cdk.json` file tells the CDK Toolkit how to execute your app.

It uses the [.NET Core CLI](https://docs.microsoft.com/dotnet/articles/core/) to compile and execute your project.

* `dotnet build src` compile this app
* `cdk deploy`       deploy this stack to your default AWS account/region
* `cdk diff`         compare deployed stack with current state
* `cdk synth`        emits the synthesized CloudFormation template