namespace _7_3
{
    class Program
    {
        class Button
        {
            public string? caption;
            public double w, h;
            public Button(string caption, int width, int height)
            {
                this.caption = caption;
                w = width;
                h = height;
            }


        }
        class CheckButton : Button
        {
            public CheckButton(string caption, int width, int height, State state) : base(caption, width, height)
            {
                this.state = state;
            }

            State state = State.Checked;

            public enum State
            {
                Checked,
                Unchecked
            }
            public override string? ToString()
            {
                return caption;
            }

            public override bool Equals(object? obj)
            {
                Button? btn = obj as Button;
                return ((btn?.caption == caption) && (btn?.h == h) && (btn?.w == w));
            }

            public void Check()
            {
                if (state == State.Checked) { 
                    state = State.Unchecked; 
                    Console.WriteLine($"Состояние: {state}"); 
                }
                else {
                    state = State.Checked;
                    Console.WriteLine($"Состояние: {state}"); 
                }
            }

            public void Zoom(int percent)
            {
                Console.WriteLine($"Исходные размеры h: {h} w: {w}");
                w = w * (1 - percent / 100.0);
                h = h * (1 - percent / 100.0);
                Console.WriteLine($"Измененные размеры h: {h} w: {w}");
            }
        }

        class User
        {
            public delegate void StateHandler2();
            public event StateHandler2? Click;

            public delegate void StateHandler(int percent);
            public event StateHandler? Resize;

            public void OnClick()
            {
                Console.WriteLine("Вызван метод Click");
                Click?.Invoke();
            }
            public void OnResize()
            {
                Console.WriteLine("Вызван метод Resize");
                Resize?.Invoke(20);
            }
        }
        static void Main(string[] args)
        {
            CheckButton btt1 = new CheckButton("Button", 3, 4, CheckButton.State.Checked);
            CheckButton btt2 = new CheckButton("Button", 3, 4, CheckButton.State.Unchecked);

            if (btt1.Equals(btt2))
            {
                Console.WriteLine("Равны");
            }
            else
            {
                Console.WriteLine("Не равны");
            }
            btt1.Check();
            btt1.Check();
            btt1.Check();
            btt1.Check();
            btt1.Check();
            Console.WriteLine("Уменьшение на заданный процент: ");
            btt1.Zoom(50);

            User user = new User();
            user.Click += btt1.Check;
            user.Resize += btt2.Zoom;
            user.OnClick();
            user.OnResize();

            LinkedList<Button> buttons = new LinkedList<Button>();
            Button button1 = new Button("Button1", 20, 30);
            Button button2 = new Button("Button2", 30, 20);
            Button button3 = new Button("Button3", 20, 20);
            Button button4 = new Button("Button4", 10, 60);
            Button button5 = new Button("Button5", 2, 30);
            Button button6 = new Button("Button6", 30, 30);

            CheckButton checkbutton1 = new CheckButton("CheckButton1", 100, 200, CheckButton.State.Checked);
            buttons.AddLast(button1);
            buttons.AddLast(button2);
            buttons.AddLast(button3);
            buttons.AddLast(button4);
            buttons.AddLast(button5);
            buttons.AddLast(button6);
            buttons.AddLast(checkbutton1);

            var s = buttons.Where(x => (x.h * x.w == 600));
            foreach (Button a in s)
            {
                Console.WriteLine($"Кнопка {a.caption} Размеры h: {a.h} w: {a.w}");
            }

            var s2 = buttons.Count(x => x is CheckButton);
            Console.WriteLine($"Количество кнопок типа CheckButton: {s2}");
        }
    }
}