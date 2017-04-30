const fs = require('fs');
const path = require('path');
const _process = require('child_process');

var integration = path.join(__dirname, 'integration');

function printError(s)
{
    console.error("\x1b[31m%s\x1b[0m", s);
}

function printSuccess(s)
{
    console.error("\x1b[32m%s\x1b[0m", s);
}

fs.readdir(integration, (err, files) => {
    files.forEach(file => {
        var filePath = path.join(integration, file);
        if(!fs.lstatSync(filePath).isDirectory() && path.extname(filePath) === ".sh")
        {
            _process.exec('sh ' + filePath, {
                    cwd: path.join(__dirname, '../bin/dotnet')
                }, function(error, stdout, stderr) {
                    var expected = JSON.parse(fs.readFileSync(path.join(integration, 'expected', file + ".log"), 'utf8'));
                    fs.writeFileSync(path.join(integration, 'actual', file + ".log"), stdout);
                    var result = "";
                    try {
                        result = JSON.parse(stdout);
                    }
                    catch(e){
                        printError(file + ": can't parse output");
                        process.exit(1);
                    }
                    if(JSON.stringify(result) == JSON.stringify(expected))
                    {
                        printSuccess(file + ": PASSED!");
                    }
                    else
                    {
                        printError(file + ": FAILED!");
                        _process.execSync('json-diff ' + path.join(integration, 'actual', file + ".log") + ' ' + path.join(integration, 'expected', file + ".log"), {stdio:[0,1,2]});
                        process.exit(1);
                    }
                }
            );
        }
    });
})