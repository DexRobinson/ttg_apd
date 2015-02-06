var PerSecond:option = option.PerSecond;

private var fps=0;

private var fpsCount=0;

private var sec:float=0;

enum option{PerDuration=0, PerSecond=1};

var duration:float = .1;

private var lastTime:float = 0;

 

function Update()

{

    if(PerSecond == option.PerSecond)

    {

        sec += Time.deltaTime;

        if(sec >= 1)

        {

            sec = 0;

            fps = fpsCount;

            fpsCount = 0;

        }

        fpsCount += 1;

    }else{

        if(lastTime >= duration)

        {

            fpsCount = 0;

            sec = 0;

            while(sec < 1)

            {

                sec += Time.deltaTime;

                fpsCount += 1;

            }

            fps = fpsCount;

            lastTime = 0;

        }else{

            lastTime += Time.deltaTime;

        }

    }

}

 

function OnGUI(){

    var width = Screen.width-100;

    var height = 10;

    GUI.Label(Rect(width,height, 100,100),"fps: " + fps);

}